using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IService
{
    public interface IUsuarioService
    {
        Task<bool> CriarUsuario(Usuario usuario);
        Task<Usuario> ConsultaUsuarioPorEmail(string email);
        Task<Usuario> ConsultaUsuarioPorId(long id);
        Task<bool> AtualizarUsuario(Usuario usuario);
        Task<bool> ExcluirUsuario(long id);
        Task<int> ContarUsuarios();
    }
}
