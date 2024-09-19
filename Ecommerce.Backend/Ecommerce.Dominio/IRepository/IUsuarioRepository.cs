using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IRepository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Retornar(long id);
        Task<Usuario> ConsultaPorEmail(string email);
        Task Incluir(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task Excluir(Usuario usuario);
    }
}
