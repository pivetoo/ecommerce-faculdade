using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;

namespace Ecommerce.Dominio.Services
{
    public class CategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<bool> CriarNovaCategoria(Categoria categoria)
        {
            await _categoriaRepository.Incluir(categoria);
            return true;
        }

        public async Task<Categoria> ConsultaCategoriaPorId(long id)
        {
            var categoria = await _categoriaRepository.Retornar(id);

            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            return categoria;
        }

        public async Task<IEnumerable<Categoria>> ListarCategorias()
        {
            return await _categoriaRepository.RetornarTodos();
        }

        public async Task<bool> AtualizarCategoria(Categoria categoria)
        {
            var categoriaExistente = await _categoriaRepository.Retornar(categoria.Id);
            if (categoriaExistente == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            categoriaExistente.Nome = categoria.Nome;

            await _categoriaRepository.Atualizar(categoriaExistente);
            return true;
        }

        public async Task<bool> ExcluirCategoria(long id)
        {
            var categoria = await _categoriaRepository.Retornar(id);

            if (categoria == null)
            {
                throw new KeyNotFoundException("Categoria não encontrada.");
            }

            await _categoriaRepository.Excluir(categoria);
            return true;
        }
    }
}
