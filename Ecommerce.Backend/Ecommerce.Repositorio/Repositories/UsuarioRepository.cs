using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EcommerceDbContext _context;

        public UsuarioRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Retornar(long id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> ConsultaPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task Incluir(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Usuario usuario)
        {
            await _context.Usuarios.FindAsync(usuario.Id);
            _context.Usuarios.Remove(usuario!);
            await _context.SaveChangesAsync();
        }
    }
}
