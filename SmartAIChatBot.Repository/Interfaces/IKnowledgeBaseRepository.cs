using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAIChatBot.Repository.Interfaces
{
    public interface IKnowledgeBaseRepository
    {
        Task UploadKnowledgeBaseAsync(IFormFile file, List<string> roles);
        Task<string> QueryKnowledgeBaseAsync(string query, string roles);
    }
}
