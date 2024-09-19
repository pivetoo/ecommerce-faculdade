using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IRepository
{
    public interface ICategoriaRepository
    {
        Task<Categoria> Retornar(long id);
        Task<IEnumerable<Categoria>> RetornarTodos();
        Task Incluir(Categoria categoria);
        Task Atualizar(Categoria categoria);
        Task Excluir(Categoria categoria);
    }
}
