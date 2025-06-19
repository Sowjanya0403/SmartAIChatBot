using Microsoft.AspNetCore.Http;
using SmartAIChatBot.Business.Interfaces;
using SmartAIChatBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAIChatBot.Services.Implementation
{
    public class KnowledgeBaseService : IKnowledgeBaseService
    {
        private readonly IKnowledgeBaseManager _knowledgeBaseManager;
        public KnowledgeBaseService(IKnowledgeBaseManager knowledgeBaseManager)
        {
            _knowledgeBaseManager = knowledgeBaseManager;
        }
        public async Task UploadKnowledgeBaseAsync(IFormFile file, List<string> roles)
        {
            await _knowledgeBaseManager.UploadKnowledgeBaseAsync(file, roles);
        }
        public async Task<string> QueryKnowledgeBaseAsync(string query, string roles)
        {
            return await _knowledgeBaseManager.QueryKnowledgeBaseAsync(query, roles);
        }
    }
}
