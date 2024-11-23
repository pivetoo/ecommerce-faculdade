using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IService;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCategoria([FromBody] Categoria categoria)
        {
            var sucesso = await _categoriaService.CriarNovaCategoria(categoria);
            return Ok(sucesso);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ConsultaCategoriaPorId(long id)
        {
            var categoria = await _categoriaService.ConsultaCategoriaPorId(id);
            return Ok(categoria);
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategorias()
        {
            var categorias = await _categoriaService.ListarCategorias();
            return Ok(categorias);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCategoria(long id, [FromBody] Categoria categoria)
        {
            var sucesso = await _categoriaService.AtualizarCategoria(categoria);
            return Ok(sucesso);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirCategoria(long id)
        {
            var sucesso = await _categoriaService.ExcluirCategoria(id);
            return Ok(sucesso);
        }
    }
}
