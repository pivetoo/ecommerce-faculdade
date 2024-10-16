using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IService
{
    public interface IChatbotContentService
    {
        Task<bool> AdicionarConteudo(ChatbotContent chatBot);
        Task<IEnumerable<ChatbotContent>> ListarConteudos();
        Task<ChatbotContent> ConsultarConteudoPorId(long id);
        Task<bool> AtualizarConteudo(ChatbotContent chatBot);
        Task<bool> ExcluirConteudo(long id);
        Task<int> ContarConteudos();
    }
}
