using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.IService;

namespace Ecommerce.Application.Services
{
    public class ChatbotContentService : IChatbotContentService
    {
        private readonly IChatbotContentRepository _chatbotContentRepository;

        public ChatbotContentService(IChatbotContentRepository chatbotContentRepository)
        {
            _chatbotContentRepository = chatbotContentRepository;
        }

        public async Task<bool> AdicionarConteudo(ChatbotContent chatBot)
        {
            await _chatbotContentRepository.Incluir(chatBot);
            return true;
        }

        public async Task<IEnumerable<ChatbotContent>> ListarConteudos()
        {
            return await _chatbotContentRepository.RetornarTodos();
        }

        public async Task<ChatbotContent> ConsultarConteudoPorId(long id)
        {
            return await _chatbotContentRepository.Retornar(id);
        }

        public async Task<bool> AtualizarConteudo(ChatbotContent chatBot)
        {
            var conteudoExistente = await _chatbotContentRepository.Retornar(chatBot.Id);
            if (conteudoExistente == null)
            {
                return false;
            }

            conteudoExistente.Role = chatBot.Role;
            conteudoExistente.Content = chatBot.Content;

            await _chatbotContentRepository.Atualizar(conteudoExistente);
            return true;
        }

        public async Task<bool> ExcluirConteudo(long id)
        {
            var conteudo = await _chatbotContentRepository.Retornar(id);
            if (conteudo == null)
            {
                return false;
            }

            await _chatbotContentRepository.Excluir(conteudo);
            return true;
        }

        public async Task<int> ContarConteudos()
        {
            return await _chatbotContentRepository.ContarConteudos();
        }
    }
}
