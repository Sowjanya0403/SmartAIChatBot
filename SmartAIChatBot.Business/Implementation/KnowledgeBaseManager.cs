using Microsoft.AspNetCore.Http;
using SmartAIChatBot.Business.Interfaces;
using SmartAIChatBot.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAIChatBot.Business.Implementation
{
    public class KnowledgeBaseManager : IKnowledgeBaseManager
    {
        private readonly IKnowledgeBaseRepository _knowledgeBaseRepository;
        public KnowledgeBaseManager(IKnowledgeBaseRepository knowledgeBaseRepository)
        {
            _knowledgeBaseRepository = knowledgeBaseRepository;
        }
        public async Task UploadKnowledgeBaseAsync(IFormFile file, List<string> roles)
        {
            if (roles == null || !roles.Any() || file == null || file.Length == 0)
            {
                throw new ArgumentNullException("roles cannot be null or file should be uploaded.");
            }
            await _knowledgeBaseRepository.UploadKnowledgeBaseAsync(file, roles);
        }
        public async Task<string> QueryKnowledgeBaseAsync(string query, string roles)
        {
            return await _knowledgeBaseRepository.QueryKnowledgeBaseAsync(query, roles);
        }
    }
}
