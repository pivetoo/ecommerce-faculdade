using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        private readonly IWebHostEnvironment _env;

        public ProdutoController(IProdutoService produtoService, IWebHostEnvironment env = null)
        {
            _produtoService = produtoService;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CriarProduto(Produto produto)
        {
            await _produtoService.CriarNovoProduto(produto);
            return Ok("Produto criado com sucesso!");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> ListarProdutoPorId(long id)
        {
            var produto = await _produtoService.ConsultaProdutoPorId(id);
            return Ok(produto);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ListarTodosProdutos()
        {
            var produtos = await _produtoService.ListarTodosProdutos();
            return Ok(produtos);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> AtualizarProduto(Produto produto)
        {
            await _produtoService.AtualizarProduto(produto);
            return Ok("Produto atualizado com sucesso!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProduto(long id)
        {
            await _produtoService.ExcluirProduto(id);
            return Ok("Produto excluído com sucesso!");
        }
    }
}
