using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IRepository
{
    public interface IFaqRepository
    {
        Task<FAQ> Retornar(long id);
        Task<IEnumerable<FAQ>> RetornarTodos();
        Task Incluir(FAQ faq);
        Task Atualizar(FAQ faq);
        Task Excluir(long id);
        Task<int> ContarFAQs();
    }
}
