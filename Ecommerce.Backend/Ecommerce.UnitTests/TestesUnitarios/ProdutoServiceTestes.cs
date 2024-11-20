using Ecommerce.Application.Services;
using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Moq;

namespace Ecommerce.UnitTests.TestesUnitarios
{
    public class ProdutoServiceTests
    {
        private Mock<IProdutoRepository> _produtoRepositoryMock;
        private ProdutoService _produtoService;

        [SetUp]
        public void SetUp()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object);
        }

        [Test]
        public async Task ListarTodosProdutos()
        {
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Descricao = "Descricao 1", Preco = 10, Estoque = 5 },
                new Produto { Id = 2, Nome = "Produto 2", Descricao = "Descricao 2", Preco = 20, Estoque = 10 }
            };
            _produtoRepositoryMock.Setup(repo => repo.RetornarTodos()).ReturnsAsync(produtos);

            var result = await _produtoService.ListarTodosProdutos();

            Assert.AreEqual(produtos, result);
        }

        [Test]
        public async Task ConsultaProdutoPorId()
        {
            var produto = new Produto { Id = 1, Nome = "Produto Teste", Descricao = "Descricao Teste", Preco = 15, Estoque = 8 };
            _produtoRepositoryMock.Setup(repo => repo.Retornar(produto.Id)).ReturnsAsync(produto);

            var result = await _produtoService.ConsultaProdutoPorId(produto.Id);

            Assert.AreEqual(produto, result);
        }

        [Test]
        public void ConsultaProdutoPorId_Exception()
        {
            _produtoRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((Produto)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _produtoService.ConsultaProdutoPorId(999));
        }

        [Test]
        public async Task AtualizarProduto()
        {
            var existingProduto = new Produto { Id = 1, Nome = "Produto Antigo", Descricao = "Descricao Antiga", Preco = 10, Estoque = 5 };
            var updatedProduto = new Produto { Id = 1, Nome = "Produto Atualizado", Descricao = "Descricao Atualizada", Preco = 20, Estoque = 10 };

            _produtoRepositoryMock.Setup(repo => repo.Retornar(existingProduto.Id)).ReturnsAsync(existingProduto);
            _produtoRepositoryMock.Setup(repo => repo.Atualizar(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            var result = await _produtoService.AtualizarProduto(updatedProduto);

            _produtoRepositoryMock.Verify(repo => repo.Atualizar(It.Is<Produto>(p => p.Nome == "Produto Atualizado" && p.Preco == 20 && p.Estoque == 10)), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void AtualizarProduto_Exception()
        {
            _produtoRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((Produto)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _produtoService.AtualizarProduto(new Produto { Id = 999 }));
        }

        [Test]
        public async Task ExcluirProduto()
        {
            var produto = new Produto { Id = 1, Nome = "Produto Teste", Descricao = "Descricao Teste" };
            _produtoRepositoryMock.Setup(repo => repo.Retornar(produto.Id)).ReturnsAsync(produto);
            _produtoRepositoryMock.Setup(repo => repo.Excluir(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            var result = await _produtoService.ExcluirProduto(produto.Id);

            _produtoRepositoryMock.Verify(repo => repo.Excluir(produto), Times.Once);
            Assert.IsTrue(result);
        }

        [Test]
        public void ExcluirProduto_Exception()
        {
            _produtoRepositoryMock.Setup(repo => repo.Retornar(It.IsAny<long>())).ReturnsAsync((Produto)null);

            Assert.ThrowsAsync<KeyNotFoundException>(() => _produtoService.ExcluirProduto(999));
        }
    }
}
