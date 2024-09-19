using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositorio.Repositories
{
    public class ChatbotContentRepository : IChatbotContentRepository
    {
        private readonly EcommerceDbContext _context;

        public ChatbotContentRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<ChatbotContent> Retornar(long id)
        {
            return await _context.ChatbotContents.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<ChatbotContent>> RetornarTodos()
        {
            return await _context.ChatbotContents.ToListAsync();
        }

        public async Task Incluir(ChatbotContent chatBot)
        {
            await _context.ChatbotContents.AddAsync(chatBot);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(ChatbotContent chatBot)
        {
            _context.ChatbotContents.Update(chatBot);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(ChatbotContent chatBot)
        {
            _context.ChatbotContents.Remove(chatBot);
            await _context.SaveChangesAsync();
        }
    }
}
