using Ecommerce.API.DTOs;
using Ecommerce.Dominio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly CarrinhoService _carrinhoService;

        public CarrinhoController(CarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObterCarrinhoPorUsuario(long usuarioId)
        {
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);
            return Ok(carrinho);
        }

        [Authorize]
        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarProdutoAoCarrinho(CarrinhoDTO carrinhoDto)
        {
            await _carrinhoService.AdicionarProdutoCarrinho(carrinhoDto.UsuarioId, carrinhoDto.Produto, carrinhoDto.Quantidade);
            return Ok("Produto adicionado ao carrinho com sucesso!");
        }

        [Authorize]
        [HttpDelete("remover")]
        public async Task<IActionResult> RemoverProdutoDoCarrinho(CarrinhoDTO carrinhoDto)
        {
            await _carrinhoService.RemoverProdutoCarrinho(carrinhoDto.UsuarioId, carrinhoDto.ProdutoId);
            return Ok("Produto removido do carrinho com sucesso!");
        }

        [Authorize]
        [HttpPost("finalizar-compra")]
        public async Task<IActionResult> FinalizarCompra(long usuarioId)
        {
            var pedido = await _carrinhoService.ConverterCarrinhoEmPedido(usuarioId);
            return Ok(pedido);
        }
    }
}
