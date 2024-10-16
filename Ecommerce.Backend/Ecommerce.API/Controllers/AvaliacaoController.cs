using Ecommerce.Application.DTOs;
using Ecommerce.Dominio.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly IAvaliacaoService _avaliacaoService;

        public AvaliacaoController(IAvaliacaoService avaliacaoService)
        {
            _avaliacaoService = avaliacaoService;
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodasAvaliacoes()
        {
            var avaliacoes = await _avaliacaoService.ListarTodasAvaliacoes();
            return Ok(avaliacoes);
        }

        [HttpGet("produto/{produtoId}")]
        public async Task<IActionResult> ListarAvaliacoesPorProduto(long produtoId)
        {
            var avaliacoes = await _avaliacaoService.ListarAvaliacoesPorProduto(produtoId);
            return Ok(avaliacoes);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAvaliacao(AvaliacaoDTO avaliacaoDto)
        {
            await _avaliacaoService.AdicionarAvaliacao(avaliacaoDto.UsuarioId, avaliacaoDto.ProdutoId, avaliacaoDto.Nota, avaliacaoDto.Comentario);
            return Ok("Avaliação adicionada com sucesso!");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarAvaliacao(long id, AvaliacaoDTO avaliacaoDto)
        {
            await _avaliacaoService.AtualizarAvaliacao(id, avaliacaoDto.Nota, avaliacaoDto.Comentario);
            return Ok("Avaliação atualizada com sucesso!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirAvaliacao(long id)
        {
            await _avaliacaoService.ExcluirAvaliacao(id);
            return Ok("Avaliação excluída com sucesso!");
        }
    }
}
