using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IRepository
{
    public interface ICarrinhoRepository
    {
        Task<Carrinho> RetornarPorUsuario(long usuarioId);
        Task Incluir(Carrinho carrinho);
        Task Atualizar(Carrinho carrinho);
        Task Remover(Carrinho carrinho);
    }
}
