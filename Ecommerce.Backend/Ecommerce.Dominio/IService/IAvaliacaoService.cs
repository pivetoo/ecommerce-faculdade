using Ecommerce.Dominio.Entities;

namespace Ecommerce.Dominio.IService
{
    public interface IAvaliacaoService
    {
        Task<bool> AdicionarAvaliacao(long usuarioId, long produtoId, int nota, string comentario);
        Task<IEnumerable<Avaliacao>> ListarTodasAvaliacoes();
        Task<IEnumerable<Avaliacao>> ListarAvaliacoesPorProduto(long produtoId);
        Task<bool> AtualizarAvaliacao(long avaliacaoId, int nota, string comentario);
        Task<bool> ExcluirAvaliacao(long avaliacaoId);
        Task AtualizarNotaMedia(Produto produto);
    }
}