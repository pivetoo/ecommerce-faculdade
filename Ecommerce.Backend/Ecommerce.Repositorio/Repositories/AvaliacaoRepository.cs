using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly EcommerceDbContext _context;

        public AvaliacaoRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<Avaliacao> Retornar(long id)
        {
            return await _context.Avaliacoes.FindAsync(id);
        }

        public async Task<IEnumerable<Avaliacao>> RetornarTodos()
        {
            return await _context.Avaliacoes.ToListAsync();
        }

        public async Task<IEnumerable<Avaliacao>> Consulta(Func<Avaliacao, bool> predicate)
        {
            return await Task.Run(() => _context.Avaliacoes.Where(predicate).ToList());
        }

        public async Task Incluir(Avaliacao avaliacao)
        {
            await _context.Avaliacoes.AddAsync(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Update(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Remove(avaliacao);
            await _context.SaveChangesAsync();
        }
    }
}
