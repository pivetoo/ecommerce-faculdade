using Ecommerce.Application.Services;
using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Ecommerce.Repositorio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.UnitTests.TestesIntegracao
{
    public class CategoriaServiceTestes
    {
        private ICategoriaRepository _categoriaRepository;
        private CategoriaService _categoriaService;
        private DbContextOptions<EcommerceDbContext> _dbContextOptions;
        private EcommerceDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=ecommercefaq;Username=postgres;Password=123;")
                .Options;
            _context = new EcommerceDbContext(_dbContextOptions);
            _categoriaRepository = new CategoriaRepository(_context);
            _categoriaService = new CategoriaService(_categoriaRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task DeveCriarNovaCategoria()
        {
            var categoria = new Categoria
            {
                Nome = "Categoria Teste"
            };

            var resultado = await _categoriaService.CriarNovaCategoria(categoria);

            Assert.IsTrue(resultado);
            var categoriaCriada = _context.Categorias.FirstOrDefault(c => c.Nome == "Categoria Teste");
            Assert.IsNotNull(categoriaCriada);
            Assert.AreEqual(categoria.Nome, categoriaCriada.Nome);
        }

        [Test]
        public async Task DeveRetornarCategoriaPorId()
        {
            var categoria = new Categoria
            {
                Nome = "Categoria Teste"
            };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            var categoriaRetornada = await _categoriaService.ConsultaCategoriaPorId(categoria.Id);

            Assert.IsNotNull(categoriaRetornada);
            Assert.AreEqual(categoria.Nome, categoriaRetornada.Nome);
        }

        [Test]
        public void DeveLancarExcecaoQuandoCategoriaNaoEncontradaPorId()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _categoriaService.ConsultaCategoriaPorId(999));
        }

        [Test]
        public async Task DeveAtualizarCategoriaExistente()
        {
            var categoria = new Categoria
            {
                Nome = "Categoria Teste"
            };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            categoria.Nome = "Categoria Atualizada";

            var resultado = await _categoriaService.AtualizarCategoria(categoria);

            Assert.IsTrue(resultado);
            var categoriaAtualizada = _context.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
            Assert.IsNotNull(categoriaAtualizada);
            Assert.AreEqual("Categoria Atualizada", categoriaAtualizada.Nome);
        }

        [Test]
        public void DeveLancarExcecaoQuandoAtualizarCategoriaNaoExistente()
        {
            var categoria = new Categoria
            {
                Id = 999,
                Nome = "Categoria Inexistente"
            };

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _categoriaService.AtualizarCategoria(categoria));
        }

        [Test]
        public async Task DeveExcluirCategoriaExistente()
        {
            var categoria = new Categoria
            {
                Nome = "Categoria Teste"
            };
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            var resultado = await _categoriaService.ExcluirCategoria(categoria.Id);

            Assert.IsTrue(resultado);
            var categoriaExcluida = _context.Categorias.FirstOrDefault(c => c.Id == categoria.Id);
            Assert.IsNull(categoriaExcluida);
        }

        [Test]
        public void DeveLancarExcecaoQuandoExcluirCategoriaNaoExistente()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _categoriaService.ExcluirCategoria(999));
        }
    }
}
