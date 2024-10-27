using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.IService;
using Ecommerce.Dominio.ValueObjects;

namespace Ecommerce.Application.Services
{
    public class ProdutoRelacaoService : IProdutoRelacaoService
    {
        private readonly IProdutoRelacaoRepository _produtoRelacaoRepository;

        public ProdutoRelacaoService(IProdutoRelacaoRepository produtoRelacaoRepository)
        {
            _produtoRelacaoRepository = produtoRelacaoRepository;
        }

        public async Task<List<Produto>> ObterProdutosEquivalentes(long produtoId)
        {
            return await _produtoRelacaoRepository.ObterProdutosRelacionados(produtoId, TipoRelacao.Equivalente);
        }

        public async Task<List<Produto>> ObterProdutosComplementares(long produtoId)
        {
            return await _produtoRelacaoRepository.ObterProdutosRelacionados(produtoId, TipoRelacao.Complementar);
        }
    }
}
