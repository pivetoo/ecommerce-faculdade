using Ecommerce.Application.DTOs;
using Ecommerce.Dominio.Util;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly ChatbotOpenAI _chatBot;

        public ChatbotController(ChatbotOpenAI chatBot)
        {
            _chatBot = chatBot;
        }

        [HttpPost("pergunta")]
        public async Task<IActionResult> FazerPergunta(PerguntaDTO perguntaDto)
        {
            try
            {
                var resposta = await _chatBot.ObterRespostaAsync(perguntaDto.Pergunta);
                return Ok(new { Resposta = resposta });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Erro ao acessar o ChatBot: {ex.Message}");
            }
        }
    }
}
