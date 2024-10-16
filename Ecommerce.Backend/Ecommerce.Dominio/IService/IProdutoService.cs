using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IService
{
    public interface IProdutoService
    {
        Task<IEnumerable<Produto>> ListarTodosProdutos();
        Task<bool> CriarNovoProduto(Produto produto);
        Task<Produto> ConsultaProdutoPorId(long id);
        Task<bool> AtualizarProduto(Produto produto);
        Task<bool> ExcluirProduto(long id);
    }
}
