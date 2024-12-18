﻿using Ecommerce.Application.DTOs;
using Ecommerce.Dominio.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [HttpGet("{usuarioId}")]
        public async Task<IActionResult> ObterCarrinhoPorUsuario(long usuarioId)
        {
            var carrinho = await _carrinhoService.ObterCarrinhoPorUsuario(usuarioId);
            return Ok(carrinho);
        }

        [HttpPost("adicionar")]
        public async Task<IActionResult> AdicionarProdutoAoCarrinho(CarrinhoDTO carrinhoDto)
        {
            await _carrinhoService.AdicionarProdutoCarrinho(carrinhoDto.UsuarioId, carrinhoDto.Produto, carrinhoDto.Quantidade);
            return Ok("Produto adicionado ao carrinho com sucesso!");
        }

        [HttpDelete("remover")]
        public async Task<IActionResult> RemoverProdutoDoCarrinho(CarrinhoDTO carrinhoDto)
        {
            await _carrinhoService.RemoverProdutoCarrinho(carrinhoDto.UsuarioId, carrinhoDto.ProdutoId);
            return Ok("Produto removido do carrinho com sucesso!");
        }

        [HttpPost("finalizar-compra")]
        public async Task<IActionResult> FinalizarCompra(long usuarioId)
        {
            var pedido = await _carrinhoService.ConverterCarrinhoEmPedido(usuarioId);
            return Ok(pedido);
        }
    }
}
