using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartAIChatBot.Services.Interfaces;

namespace SmartAIChatBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class KnowledgeBaseController : ControllerBase
    {
        private readonly IKnowledgeBaseService _knowledgeBaseService;
        public KnowledgeBaseController(IKnowledgeBaseService knowledgeBaseService)
        {
            _knowledgeBaseService = knowledgeBaseService;
        }
        //[Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> UploadKnowledgeBase(IFormFile file, [FromQuery] List<string> roles)
        {
            try
            {
                await _knowledgeBaseService.UploadKnowledgeBaseAsync(file, roles);
                return Ok("Knowledge base uploaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> QueryKnowledgeBase(string query)
        {
            try
            {
                var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
                var result = await _knowledgeBaseService.QueryKnowledgeBaseAsync(query, role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
