using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IService
{
    public interface IProdutoRelacaoService
    {
        Task<List<Produto>> ObterProdutosEquivalentes(long produtoId);
        Task<List<Produto>> ObterProdutosComplementares(long produtoId);
    }
}
