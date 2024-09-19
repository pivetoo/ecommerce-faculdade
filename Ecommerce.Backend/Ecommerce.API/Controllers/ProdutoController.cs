using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
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
