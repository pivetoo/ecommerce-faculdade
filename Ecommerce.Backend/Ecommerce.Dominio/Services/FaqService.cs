using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;

namespace Ecommerce.Dominio.Services
{
    public class FaqService
    {
        private readonly IFaqRepository _faqRepository;

        public FaqService(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        public async Task<IEnumerable<FAQ>> ListarTodosFAQs()
        {
            return await _faqRepository.RetornarTodos();
        }

        public async Task<bool> CriarFAQ(FAQ faq)
        {
            DateTime localDateTime = DateTime.Now;
            DateTime utcDateTime = localDateTime.ToUniversalTime();
            await _faqRepository.Incluir(faq);
            return true;
        }

        public async Task<bool> AtualizarFAQ(FAQ faq)
        {
            var faqExistente = await _faqRepository.Retornar(faq.Id);
            if (faqExistente == null)
            {
                throw new KeyNotFoundException("FAQ não encontrada.");
            }

            faqExistente.Titulo = faq.Titulo;
            faqExistente.Descricao = faq.Descricao;
            faqExistente.SaibaMais = faq.SaibaMais;

            await _faqRepository.Atualizar(faqExistente);
            return true;
        }

        public async Task<FAQ> ConsultarFAQPorId(long id)
        {
            var faq = await _faqRepository.Retornar(id);
            if (faq == null)
            {
                throw new KeyNotFoundException("FAQ não encontrada.");
            }

            return faq;
        }

        public async Task<bool> ExcluirFAQ(long id)
        {
            var faqExistente = await _faqRepository.Retornar(id);
            if (faqExistente == null)
            {
                throw new KeyNotFoundException("FAQ não encontrada.");
            }

            await _faqRepository.Excluir(id);
            return true;
        }
    }
}
