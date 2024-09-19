using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.Util;

namespace Ecommerce.Dominio.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> CriarUsuario(Usuario usuario)
        {            
            //var usuarioExistente = await _usuarioRepository.ConsultaPorEmail(usuario.Email);
            //if (usuarioExistente == null)
            //{
            //    throw new InvalidOperationException("Um usuário com este e-mail já existe.");
            //}

            if (!PasswordPolicy.ValidarSenha(usuario.Senha))
            {
                throw new InvalidOperationException("A senha não atende aos requisitos de segurança. A senha deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e símbolos.");
            }

            usuario.Senha = PasswordHash.HashPassword(usuario.Senha);
            await _usuarioRepository.Incluir(usuario);
            return true;
        }

        public async Task<Usuario> ConsultaUsuarioPorEmail(string email)
        {
            return await _usuarioRepository.ConsultaPorEmail(email);
        }

        public async Task<Usuario> ConsultaUsuarioPorId(long id)
        {
            var usuario = await _usuarioRepository.Retornar(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return usuario;
        }

        public async Task<bool> AtualizarUsuario(Usuario usuario)
        {
            var usuarioExistente = await _usuarioRepository.Retornar(usuario.Id);
            if (usuarioExistente == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Telefone = usuario.Telefone;
            usuarioExistente.Endereco = usuario.Endereco;

            await _usuarioRepository.Atualizar(usuarioExistente);
            return true;
        }

        public async Task<bool> ExcluirUsuario(long id)
        {
            var usuario = await _usuarioRepository.Retornar(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            await _usuarioRepository.Excluir(usuario);
            return true;
        }
    }
}
