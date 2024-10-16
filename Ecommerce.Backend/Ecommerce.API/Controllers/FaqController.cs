using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IFaqService _faqService;
        private readonly IWebHostEnvironment _env;

        public FAQController(IFaqService faqService, IWebHostEnvironment env)
        {
            _faqService = faqService;
            _env = env;
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage([FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No file uploaded.");

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            var imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

            return Ok(new { imageUrl });
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CriarFAQ(FAQ faq)
        {
            var criar = await _faqService.CriarFAQ(faq);
            return Ok(criar);
        }

        //[Authorize]
        [HttpGet]
        public async Task<IActionResult> ListarTodosFAQs(int pageNumber = 1, int pageSize = 10)
        {
            var faqsPaginados = await _faqService.ListarTodosFAQs(pageNumber, pageSize);
            return Ok(faqsPaginados);
        }

        //[Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultarFAQPorId(long id)
        {
            var faq = await _faqService.ConsultarFAQPorId(id);
            return Ok(faq);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarFAQ(long id, FAQ faq)
        {
            var atualizar = await _faqService.AtualizarFAQ(faq);
            return Ok(atualizar);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoverFAQ(long id)
        {
            var remover = await _faqService.ExcluirFAQ(id);
            return Ok(remover);
        }

        [HttpGet("count")]
        public async Task<IActionResult> ContarFAQs()
        {
            var totalFAQs = await _faqService.ContarFAQs();
            return Ok(new { total = totalFAQs });
        }
    }
}
