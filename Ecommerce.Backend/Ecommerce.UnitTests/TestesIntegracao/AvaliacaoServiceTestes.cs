using Ecommerce.Application.Services;
using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Ecommerce.Repositorio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.UnitTests.TestesIntegracao
{
    public class AvaliacaoServiceTestes
    {
        private IAvaliacaoRepository _avaliacaoRepository;
        private IUsuarioRepository _usuarioRepository;
        private IProdutoRepository _produtoRepository;
        private AvaliacaoService _avaliacaoService;
        private DbContextOptions<EcommerceDbContext> _dbContextOptions;
        private EcommerceDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=ecommercefaq;Username=postgres;Password=123;")
                .Options;
            _context = new EcommerceDbContext(_dbContextOptions);
            _avaliacaoRepository = new AvaliacaoRepository(_context);
            _usuarioRepository = new UsuarioRepository(_context);
            _produtoRepository = new ProdutoRepository(_context);
            _avaliacaoService = new AvaliacaoService(_avaliacaoRepository, _usuarioRepository, _produtoRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void DeveLancarExcecaoQuandoAdicionarAvaliacaoParaProdutoNaoExistente()
        {
            var usuario = new Usuario
            {
                Nome = "Usuario Teste",
                Email = "teste@teste.com",
                Senha = "SenhaTeste123",
                Telefone = "123456789",
                Endereco = "Endereco Teste",
                Cpf = "12345678900"
            };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _avaliacaoService.AdicionarAvaliacao(usuario.Id, 999, 5, "Ótimo produto!"));
        }
       
        [Test]
        public void DeveLancarExcecaoQuandoAtualizarAvaliacaoNaoExistente()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _avaliacaoService.AtualizarAvaliacao(999, 5, "Avaliação não encontrada."));
        }        

        [Test]
        public void DeveLancarExcecaoQuandoExcluirAvaliacaoNaoExistente()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _avaliacaoService.ExcluirAvaliacao(999));
        }
    }
}
