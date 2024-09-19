using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IRepository
{
    public interface IChatbotContentRepository
    {
        Task<ChatbotContent> Retornar(long id);
        Task<IEnumerable<ChatbotContent>> RetornarTodos();
        Task Incluir(ChatbotContent chatBot);
        Task Atualizar(ChatbotContent chatBot);
        Task Excluir(ChatbotContent chatBot);
    }
}
