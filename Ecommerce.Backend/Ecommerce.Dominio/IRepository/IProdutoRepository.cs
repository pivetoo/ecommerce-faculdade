using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IRepository
{
    public interface IProdutoRepository
    {
        Task<Produto> Retornar(long id);
        Task<IEnumerable<Produto>> RetornarTodos();
        Task<Categoria> RetornarCategoriaPorId(long id);
        Task Incluir(Produto produto);
        Task Atualizar(Produto produto);
        Task Excluir(Produto produto);
    }
}
