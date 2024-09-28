using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotContentController : ControllerBase
    {
        private readonly ChatbotContentService _chatBotService;

        public ChatbotContentController(ChatbotContentService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AdicionarConteudo(ChatbotContent content)
        {
            var adicionar = await _chatBotService.AdicionarConteudo(content);
            return Ok(adicionar);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ListarConteudos()
        {
            var listar = await _chatBotService.ListarConteudos();
            return Ok(listar);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarConteudo(long id, ChatbotContent content)
        {
            var atualizar = await _chatBotService.AtualizarConteudo(content);
            return Ok(content);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirConteudo(long id)
        {
            var excluir = await _chatBotService.ExcluirConteudo(id);
            return Ok(excluir);
        }
    }
}
