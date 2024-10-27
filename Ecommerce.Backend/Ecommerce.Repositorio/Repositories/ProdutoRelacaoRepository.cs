using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.ValueObjects;
using Ecommerce.Repositorio.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.Repositories
{
    public class ProdutoRelacaoRepository : IProdutoRelacaoRepository
    {
        private readonly EcommerceDbContext _context;

        public ProdutoRelacaoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ObterProdutosRelacionados(long produtoId, TipoRelacao tipoRelacao)
        {
            return await _context.ProdutoRelacoes
                .Where(pr => pr.ProdutoId == produtoId && pr.Tipo == tipoRelacao)
                .Select(pr => pr.ProdutoRelacionado)
                .ToListAsync();
        }
    }
}