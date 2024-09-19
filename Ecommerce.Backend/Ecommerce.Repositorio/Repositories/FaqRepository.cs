using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.Repositories
{
    public class FaqRepository : IFaqRepository
    {
        private readonly EcommerceDbContext _context;

        public FaqRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<FAQ> Retornar(long id)
        {
            return await _context.FAQs.FindAsync(id);
        }

        public async Task<IEnumerable<FAQ>> RetornarTodos()
        {
            return await _context.FAQs.ToListAsync();
        }

        public async Task Incluir(FAQ faq)
        {
            _context.FAQs.Add(faq);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(FAQ faq)
        {
            _context.FAQs.Update(faq);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(long id)
        {
            var faq = await _context.FAQs.FindAsync(id);
            _context.FAQs.Remove(faq!);
            await _context.SaveChangesAsync();
        }
    }
}
