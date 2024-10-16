using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IService
{
    public interface ICategoriaService
    {
        Task<bool> CriarNovaCategoria(Categoria categoria);
        Task<Categoria> ConsultaCategoriaPorId(long id);
        Task<IEnumerable<Categoria>> ListarCategorias();
        Task<bool> AtualizarCategoria(Categoria categoria);
        Task<bool> ExcluirCategoria(long id);
    }
}
