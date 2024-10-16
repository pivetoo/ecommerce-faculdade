using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotContentController : ControllerBase
    {
        private readonly IChatbotContentService _chatBotService;

        public ChatbotContentController(IChatbotContentService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AdicionarConteudo(ChatbotContent content)
        {
            var adicionar = await _chatBotService.AdicionarConteudo(content);
            return Ok(adicionar);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListarConteudos()
        {
            var listar = await _chatBotService.ListarConteudos();
            return Ok(listar);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarConteudo(long id, ChatbotContent content)
        {
            var atualizar = await _chatBotService.AtualizarConteudo(content);
            return Ok(content);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirConteudo(long id)
        {
            var excluir = await _chatBotService.ExcluirConteudo(id);
            return Ok(excluir);
        }

        [HttpGet("count")]
        public async Task<IActionResult> ContarConteudos()
        {
            var conteudos = await _chatBotService.ContarConteudos();
            return Ok(new { total = conteudos });
        }
    }
}
