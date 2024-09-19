using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IRepository
{
    public interface IPedidoRepository
    {
        Task<Pedido> Retornar(long id);
        Task<IEnumerable<Pedido>> ConsultaPedidosPorUsuario(long usuarioId);
        Task Incluir(Pedido pedido);
        Task Atualizar(Pedido pedido);
        Task Excluir(Pedido pedido);
    }
}
