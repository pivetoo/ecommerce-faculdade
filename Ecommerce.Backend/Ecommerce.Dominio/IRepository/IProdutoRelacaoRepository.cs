using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.ValueObjects;

namespace Ecommerce.Dominio.IRepository
{
    public interface IProdutoRelacaoRepository
    {
        Task<List<Produto>> ObterProdutosRelacionados(long produtoId, TipoRelacao tipoRelacao);
    }
}
