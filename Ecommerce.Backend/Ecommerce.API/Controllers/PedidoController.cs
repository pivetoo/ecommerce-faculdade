using Ecommerce.API.DTOs;
using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.Services;
using Ecommerce.Dominio.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoController : ControllerBase
    {
        public readonly PedidoService _pedidoService;

        public PedidoController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CriarPedido(PedidoDTO pedidoDto)
        {
            await _pedidoService.CriarPedido(pedidoDto.UsuarioId, pedidoDto.Produtos);
            return Ok("Pedido criado com sucesso!");
        }

        [Authorize]
        [HttpPost("finalizar-pedido")]
        public async Task<IActionResult> FinalizarPedido(Pedido pedido)
        {
            var pedidoFinalizado = await _pedidoService.FinalizarPedido(pedido);
            return Ok(new
            {
                Status = pedidoFinalizado.Status.ToString(),
                Pedido = pedidoFinalizado,
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> ListarPedidoPorId(long id)
        {
            var pedido = await _pedidoService.ConsultaPedidoPorId(id);
            return Ok(pedido);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> ListarPedidoPorUsuario(long usuarioId)
        {
            var pedidos = await _pedidoService.ConsultaPedidosPorUsuario(usuarioId);
            return Ok(pedidos);
        }

        [Authorize]
        [HttpPut("{pedidoId}/status")]
        public async Task<IActionResult> AtualizarStatusPedido(long pedidoId, StatusPedido novoStatus)
        {
            await _pedidoService.AtualizarStatusPedido(pedidoId, novoStatus);
            return Ok("Status do pedido atualizado com sucesso!");
        }

        [Authorize]
        [HttpDelete("{pedidoId}")]
        public async Task<IActionResult> CancelarPedido(long pedidoId)
        {
            await _pedidoService.CancelarPedido(pedidoId);
            return Ok("Pedido cancelado com sucesso!");
        }
    }
}
