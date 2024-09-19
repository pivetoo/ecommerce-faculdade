using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly EcommerceDbContext _context;

        public CarrinhoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Carrinho> RetornarPorUsuario(long usuarioId)
        {
            return await _context.Carrinhos.Include(c => c.Itens).ThenInclude(i => i.Produto).FirstOrDefaultAsync(c => c.Usuario.Id == usuarioId);
        }

        public async Task Incluir(Carrinho carrinho)
        {
            await _context.Carrinhos.AddAsync(carrinho);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Carrinho carrinho)
        {
            _context.Carrinhos.Update(carrinho);
            await _context.SaveChangesAsync();
        }

        public async Task Remover(Carrinho carrinho)
        {
            _context.Carrinhos.Remove(carrinho);
            await _context.SaveChangesAsync();
        }
    }
}
