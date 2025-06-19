using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAIChatBot.Services.Interfaces
{
    public  interface IKnowledgeBaseService
    {
        Task UploadKnowledgeBaseAsync(IFormFile file, List<string> roles);
        Task<string> QueryKnowledgeBaseAsync(string query, string roles);
    }
}
