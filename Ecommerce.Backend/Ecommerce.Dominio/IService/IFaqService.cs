using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IService
{
    public interface IFaqService
    {
        Task<object> ListarTodosFAQs(int pageNumber, int pageSize);
        Task<bool> CriarFAQ(FAQ faq);
        Task<bool> AtualizarFAQ(FAQ faq);
        Task<FAQ> ConsultarFAQPorId(long id);
        Task<bool> ExcluirFAQ(long id);
        Task<int> ContarFAQs();
    }
}
