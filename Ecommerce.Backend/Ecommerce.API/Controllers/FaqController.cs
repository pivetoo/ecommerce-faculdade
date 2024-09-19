using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly FaqService _faqService;

        public FAQController(FaqService faqService)
        {
            _faqService = faqService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CriarFAQ(FAQ faq)
        {
            var criar = await _faqService.CriarFAQ(faq);
            return Ok(criar);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListarTodosFAQs()
        {
            var faqs = await _faqService.ListarTodosFAQs();
            return Ok(faqs);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarFAQPorId(long id)
        {
            var faq = await _faqService.ConsultarFAQPorId(id);
            return Ok(faq);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFAQ(long id, FAQ faq)
        {
            var atualizar = await _faqService.AtualizarFAQ(faq);
            return Ok(atualizar);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> RemoverFAQ(long id)
        {
            var remover = await _faqService.ExcluirFAQ(id);
            return Ok(remover);
        }
    }
}
