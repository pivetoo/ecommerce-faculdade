using Ecommerce.Dominio.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoRelacaoController : ControllerBase
    {
        private readonly IProdutoRelacaoService _produtoRelacaoService;

        public ProdutoRelacaoController(IProdutoRelacaoService produtoRelacaoService)
        {
            _produtoRelacaoService = produtoRelacaoService;
        }

        [HttpGet("{produtoId}/equivalentes")]
        public async Task<IActionResult> ObterProdutosEquivalentes(long produtoId)
        {
            var produtosEquivalentes = await _produtoRelacaoService.ObterProdutosEquivalentes(produtoId);
            if (produtosEquivalentes == null || !produtosEquivalentes.Any())
            {
                return NotFound("Nenhum produto equivalente encontrado.");
            }
            return Ok(produtosEquivalentes);
        }

        [HttpGet("{produtoId}/complementares")]
        public async Task<IActionResult> ObterProdutosComplementares(long produtoId)
        {
            var produtosComplementares = await _produtoRelacaoService.ObterProdutosComplementares(produtoId);
            if (produtosComplementares == null || !produtosComplementares.Any())
            {
                return NotFound("Nenhum produto complementar encontrado.");
            }
            return Ok(produtosComplementares);
        }
    }
}
