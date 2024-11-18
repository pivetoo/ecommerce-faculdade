using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly EcommerceDbContext _context;

        public ProdutoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> Retornar(long id)
        {
            return await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public async Task<Categoria> RetornarCategoriaPorId(long id)
        {
            return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task<IEnumerable<Produto>> RetornarTodos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task Incluir(Produto produto)
        {
            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Produto produto)
        {
            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Produto produto)
        {
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
        }
    }
}
