using Azure;
using Azure.AI.OpenAI;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using OpenAI;
using OpenAI.Chat;
using SmartAIChatBot.Repository.Interfaces;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartAIChatBot.Repository.Implementation
{
    public class KnowledgeBaseRepository : IKnowledgeBaseRepository
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly string _serviceEndpoint;
        private readonly string _indexName;
        private readonly string _apiKey;
        private readonly SearchClient _searchClient;
        private readonly AzureOpenAIClient _azureClient;
        private readonly ChatClient _chatClient;
        public KnowledgeBaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration["AzureBlobStorage:ConnectionString"];
            _containerName = configuration["AzureBlobStorage:ContainerName"];
            _serviceEndpoint = configuration["AzureAISearch:Endpoint"];
            _indexName = configuration["AzureAISearch:indexName"];
            _apiKey = configuration["AzureAISearch:ApiKey"];
            _searchClient = new SearchClient(new Uri(_serviceEndpoint), _indexName, new AzureKeyCredential(_apiKey));

            // Create OpenAI client
            _azureClient = new AzureOpenAIClient(
            new Uri(configuration["OpenAI:Endpoint"]),
            new ApiKeyCredential(configuration["OpenAI:ApiKey"]));
            _chatClient = _azureClient.GetChatClient(configuration["OpenAI:DeploymentName"]);
        }


        public async Task UploadKnowledgeBaseAsync(IFormFile file, List<string> roles)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is required.");

            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);

            // Ensure the container exists
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(file.FileName);

            // Upload the file
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            // Set roles as metadata
            var metadata = new Dictionary<string, string>
            {
                { "Roles", JsonSerializer.Serialize(roles) }
            };
            await blobClient.SetMetadataAsync(metadata);
        }
        public async Task<string> QueryKnowledgeBaseAsync(string query, string roles)
        {

            var results = new List<Object>();
            results = await AzureAISearch(query, roles);
                if (results.Count() == 0)
                {
                    throw new Exception($"No results found in the knowledge base for query {query}");
                }
            // Build chat messages using the correct factory methods
            var messages = new List<OpenAI.Chat.ChatMessage>
                {
                    OpenAI.Chat.ChatMessage.CreateSystemMessage(
                        "You are a helpful assistant who answers questions based only on the given context."),
                    OpenAI.Chat.ChatMessage.CreateUserMessage(
                        $"Context: {JsonSerializer.Serialize(results)}\n\nQuestion: {query}")
                };

            // 4. Send to OpenAI and get response
            var chatResponse = await _chatClient.CompleteChatAsync(messages);

            return chatResponse.Value.Content.Last().Text ;
        }

        public async Task<List<Object>> AzureAISearch(string query, string roles)
        {
            var options = new SearchOptions()
            {
                Filter = $"Roles/any(r: r eq '{roles}')",
                SearchFields = { "Content" },
                Select = { "Content", "Roles" },
                IncludeTotalCount = true
            };

            var response = await _searchClient.SearchAsync<SearchDocument>(query, options);

            var results = new List<object>();
            await foreach (var result in response.Value.GetResultsAsync())
            {
                results.Add(new
                {
                    Content = result.Document["Content"]
                });
            }

            return results;
        }
    }

}
