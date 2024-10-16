using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IService
{
    public interface ICarrinhoService
    {
        Task<bool> AdicionarProdutoCarrinho(long usuarioId, Produto produto, int quantidade);
        Task<bool> RemoverProdutoCarrinho(long usuarioId, long produtoId);
        Task<Carrinho> ObterCarrinhoPorUsuario(long usuarioId);
        Task<Pedido> ConverterCarrinhoEmPedido(long usuarioId);
    }
}
