using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IRepository
{
    public interface IAvaliacaoRepository
    {
        Task<Avaliacao> Retornar(long id);
        Task<IEnumerable<Avaliacao>> RetornarTodos();
        Task<IEnumerable<Avaliacao>> Consulta(Func<Avaliacao, bool> predicate);
        Task Incluir(Avaliacao avaliacao);
        Task Atualizar(Avaliacao avaliacao);
        Task Excluir(Avaliacao avaliacao);
    }
}
