using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;

namespace Ecommerce.Dominio.Services
{
    public class AvaliacaoService
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProdutoRepository _produtoRepository;

        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository, IUsuarioRepository usuarioRepository, IProdutoRepository produtoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
            _usuarioRepository = usuarioRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> AdicionarAvaliacao(long usuarioId, long produtoId, int nota, string comentario)
        {
            var usuario = await _usuarioRepository.Retornar(usuarioId);
            var produto = await _produtoRepository.Retornar(produtoId);

            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            var avaliacao = new Avaliacao
            {
                Usuario = usuario,
                Produto = produto,
                Nota = nota,
                Comentario = comentario,
                DataAvaliacao = DateTime.Now
            };

            await _avaliacaoRepository.Incluir(avaliacao);
            return true;
        }

        public async Task<IEnumerable<Avaliacao>> ListarTodasAvaliacoes()
        {
            return await _avaliacaoRepository.RetornarTodos();
        }

        public async Task<IEnumerable<Avaliacao>> ListarAvaliacoesPorProduto(long produtoId)
        {
            return await _avaliacaoRepository.Consulta(p => p.Produto.Id == produtoId);
        }

        public async Task<bool> AtualizarAvaliacao(long avaliacaoId, int nota, string comentario)
        {
            var avaliacao = await _avaliacaoRepository.Retornar(avaliacaoId);
            if (avaliacao == null)
            {
                throw new KeyNotFoundException("Avaliação não encontrada.");
            }

            avaliacao.Nota = nota;
            avaliacao.Comentario = comentario;

            await _avaliacaoRepository.Atualizar(avaliacao);
            return true;
        }

        public async Task<bool> ExcluirAvaliacao(long avaliacaoId)
        {
            var avaliacao = await _avaliacaoRepository.Retornar(avaliacaoId);
            if (avaliacao == null)
            {
                throw new KeyNotFoundException("Avaliação não encontrada.");
            }

            await _avaliacaoRepository.Excluir(avaliacao);
            return true;
        }

        public async Task AtualizarNotaMedia(Produto produto)
        {
            var avaliacao = await _avaliacaoRepository.Consulta(p => p.Produto.Id == produto.Id);
            if (avaliacao == null)
            {
                throw new KeyNotFoundException("Avaliação não encontrada.");
            }

            await _avaliacaoRepository.Consulta(p => p.Produto.Id == produto.Id);

            produto.NotaMedia = avaliacao.Any() ? avaliacao.Average(a => a.Nota) : 0;
            
            await _produtoRepository.Atualizar(produto);
        }
    }
}
