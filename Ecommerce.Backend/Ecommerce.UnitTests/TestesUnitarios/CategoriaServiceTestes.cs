using Ecommerce.Application.Services;
using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Moq;

namespace Ecommerce.UnitTests.TestesUnitarios
{
    public class CategoriaServiceTests
    {
        private Mock<ICategoriaRepository> _categoriaRepositoryMock;
        private CategoriaService _categoriaService;

        [SetUp]
        public void SetUp()
        {
            _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
            _categoriaService = new CategoriaService(_categoriaRepositoryMock.Object);
        }

        [Test]
        public async Task CriarNovaCategoria()
        {
            var categoria = new Categoria { Id = 1, Nome = "Categoria Teste" };
            _categoriaRepositoryMock.Setup(repo => repo.Incluir(It.IsAny<Categoria>())).Returns(Task.CompletedTask);

            var result = await _categoriaService.CriarNovaCategoria(categoria);

            _categoriaRepositoryMock.Verify(repo => repo.Incluir(categoria), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ConsultaCategoriaPorId()
        {
            var categoria = new Categoria { Id = 1, Nome = "Categoria Teste" };
            _categoriaRepositoryMock.Setup(repo => repo.Retornar(categoria.Id)).ReturnsAsync(categoria);

            var result = await _categoriaService.ConsultaCategoriaPorId(categoria.Id);

            Assert.AreEqual(categoria, result);
        }

        [Test]
        public void ConsultaCategoriaPorId_Exception()
        {
            _categoriaRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((Categoria)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _categoriaService.ConsultaCategoriaPorId(999));
        }

        [Test]
        public async Task ListarCategorias()
        {
            var categorias = new List<Categoria>
            {
                new Categoria { Id = 1, Nome = "Categoria 1" },
                new Categoria { Id = 2, Nome = "Categoria 2" }
            };
            _categoriaRepositoryMock.Setup(repo => repo.RetornarTodos()).ReturnsAsync(categorias);

            var result = await _categoriaService.ListarCategorias();

            Assert.AreEqual(categorias, result);
        }

        [Test]
        public async Task AtualizarCategoria()
        {
            var existingCategoria = new Categoria { Id = 1, Nome = "Categoria Antiga" };
            var updatedCategoria = new Categoria { Id = 1, Nome = "Categoria Atualizada" };

            _categoriaRepositoryMock.Setup(repo => repo.Retornar(existingCategoria.Id)).ReturnsAsync(existingCategoria);
            _categoriaRepositoryMock.Setup(repo => repo.Atualizar(It.IsAny<Categoria>())).Returns(Task.CompletedTask);

            var result = await _categoriaService.AtualizarCategoria(updatedCategoria);

            _categoriaRepositoryMock.Verify(repo => repo.Atualizar(It.Is<Categoria>(c => c.Nome == "Categoria Atualizada")), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void AtualizarCategoria_Exception()
        {
            _categoriaRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((Categoria)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _categoriaService.AtualizarCategoria(new Categoria { Id = 999 }));
        }

        [Test]
        public async Task ExcluirCategoria()
        {
            var categoria = new Categoria { Id = 1, Nome = "Categoria Teste" };
            _categoriaRepositoryMock.Setup(repo => repo.Retornar(categoria.Id)).ReturnsAsync(categoria);
            _categoriaRepositoryMock.Setup(repo => repo.Excluir(It.IsAny<Categoria>())).Returns(Task.CompletedTask);

            var result = await _categoriaService.ExcluirCategoria(categoria.Id);

            _categoriaRepositoryMock.Verify(repo => repo.Excluir(categoria), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void ExcluirCategoria_Exception()
        {
            _categoriaRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((Categoria)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _categoriaService.ExcluirCategoria(999));
        }
    }
}
