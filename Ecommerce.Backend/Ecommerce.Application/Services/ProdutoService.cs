using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.IService;

namespace Ecommerce.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<IEnumerable<Produto>> ListarTodosProdutos()
        {
            return await _produtoRepository.RetornarTodos();
        }

        public async Task<bool> CriarNovoProduto(Produto produto)
        {
            var categoria = await _produtoRepository.RetornarCategoriaPorId(produto.CategoriaId);
            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            produto.Categoria = categoria;
            await _produtoRepository.Incluir(produto);
            return true;
        }

        public async Task<Produto> ConsultaProdutoPorId(long id)
        {
            var produto = await _produtoRepository.Retornar(id);
            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            return produto;
        }

        public async Task<bool> AtualizarProduto(Produto produto)
        {
            var produtoExistente = await _produtoRepository.Retornar(produto.Id);
            if (produtoExistente == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            produtoExistente.Nome = produto.Nome;
            produtoExistente.Descricao = produto.Descricao;
            produtoExistente.Preco = produto.Preco;
            produtoExistente.Estoque = produto.Estoque;

            await _produtoRepository.Atualizar(produtoExistente);
            return true;
        }

        public async Task<bool> ExcluirProduto(long id)
        {
            var produto = await _produtoRepository.Retornar(id);
            if (produto == null)
            {
                throw new KeyNotFoundException("Produto não encontrado.");
            }

            await _produtoRepository.Excluir(produto);
            return true;
        }
    }
}
