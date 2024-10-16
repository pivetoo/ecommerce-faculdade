using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.ValueObjects;

namespace Ecommerce.Dominio.IService
{
    public interface IPedidoService
    {
        Task<bool> CriarPedido(long usuarioId, List<Produto> produtos);
        Task<Pedido> FinalizarPedido(Pedido pedido);
        Task<Pedido> ConsultaPedidoPorId(long id);
        Task<IEnumerable<Pedido>> ConsultaPedidosPorUsuario(long usuarioId);
        Task<bool> AtualizarStatusPedido(long pedidoId, StatusPedido novoStatus);
        Task CancelarPedido(long pedidoId);
    }
}
