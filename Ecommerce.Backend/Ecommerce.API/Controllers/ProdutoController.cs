using Ecommerce.Application.DTOs;
using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IService;
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

        [HttpPost]
        public async Task<IActionResult> CriarProduto(ProdutoDTO produtoDto)
        {
            var produto = new Produto
            {
                Nome = produtoDto.Nome,
                Descricao = produtoDto.Descricao,
                Preco = produtoDto.Preco,
                ImagemUrl = produtoDto.ImagemUrl,
                Quantidade = produtoDto.Quantidade,
                Estoque = produtoDto.Estoque,
                CategoriaId = produtoDto.CategoriaId
            };

            await _produtoService.CriarNovoProduto(produto);
            return Ok("Produto criado com sucesso!");
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ListarProdutoPorId(long id)
        {
            var produto = await _produtoService.ConsultaProdutoPorId(id);
            return Ok(produto);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodosProdutos()
        {
            var produtos = await _produtoService.ListarTodosProdutos();
            return Ok(produtos);
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarProduto(Produto produto)
        {
            await _produtoService.AtualizarProduto(produto);
            return Ok("Produto atualizado com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirProduto(long id)
        {
            await _produtoService.ExcluirProduto(id);
            return Ok("Produto excluído com sucesso!");
        }
    }
}
