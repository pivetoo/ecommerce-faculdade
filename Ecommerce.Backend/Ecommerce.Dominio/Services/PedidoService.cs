using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Dominio.Services
{
    public class PedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly PagamentoService _pagamentoService;

        public PedidoService(IPedidoRepository pedidoRepository, IUsuarioRepository usuarioRepository, PagamentoService pagamentoService)
        {
            _pedidoRepository = pedidoRepository;
            _usuarioRepository = usuarioRepository;
            _pagamentoService = pagamentoService;
        }

        public async Task<bool> CriarPedido(long usuarioId, List<Produto> produtos)
        {
            var usuario = await _usuarioRepository.Retornar(usuarioId);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            var pedido = new Pedido
            {
                Usuario = usuario,
                Produtos = produtos,
                DataPedido = DateTime.Now,
                Status = StatusPedido.Pendente,
                Total = produtos.Sum(p => p.Preco * p.Quantidade)
            };

            await _pedidoRepository.Incluir(pedido);
            return true;
        }

        public async Task<Pedido> FinalizarPedido(Pedido pedido)
        {
            var resultadoPagamento = _pagamentoService.ProcessarPagamento(pedido.Total);
            if (resultadoPagamento == "Sucesso")
            {
                pedido.Status = StatusPedido.Pago;
            }
            else
            {
                pedido.Status = StatusPedido.Pendente;
            }

            await _pedidoRepository.Atualizar(pedido);
            return pedido;
        }

        public async Task<Pedido> ConsultaPedidoPorId(long id)
        {
            var pedido = await _pedidoRepository.Retornar(id);
            if (pedido == null)
            {
                throw new KeyNotFoundException("Pedido não encontrado.");
            }

            return pedido;
        }

        public async Task<IEnumerable<Pedido>> ConsultaPedidosPorUsuario(long usuarioId)
        {
            return await _pedidoRepository.ConsultaPedidosPorUsuario(usuarioId);
        }

        public async Task<bool> AtualizarStatusPedido(long pedidoId, StatusPedido novoStatus)
        {
            var pedido = await _pedidoRepository.Retornar(pedidoId);
            if (pedido == null)
            {
                throw new KeyNotFoundException("Pedido não encontrado.");
            }

            pedido.Status = novoStatus;
            await _pedidoRepository.Atualizar(pedido);

            return true;
        }

        public async Task CancelarPedido(long pedidoId)
        {
            await AtualizarStatusPedido(pedidoId, StatusPedido.Cancelado);
        }
    }
}
