using Ecommerce.Application.Services;
using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Repositorio.Data;
using Ecommerce.Repositorio.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.UnitTests.TestesIntegracao
{
    public class UsuarioServiceTestes
    {
        private IUsuarioRepository _usuarioRepository;
        private UsuarioService _usuarioService;
        private DbContextOptions<EcommerceDbContext> _dbContextOptions;
        private EcommerceDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _dbContextOptions = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseNpgsql("Host=localhost;Port=5432;Database=ecommercefaq;Username=postgres;Password=123;")
                .Options;
            _context = new EcommerceDbContext(_dbContextOptions);
            _usuarioRepository = new UsuarioRepository(_context);
            _usuarioService = new UsuarioService(_usuarioRepository);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public void DeveLancarExcecaoQuandoCriarUsuarioComEmailExistente()
        {
            var usuarioExistente = new Usuario
            {
                Nome = "Usuario Existente",
                Email = "existente@teste.com",
                Senha = "SenhaExistente123",
                Telefone = "123456789",
                Endereco = "Endereco Existente",
                Cpf = "12345678901"
            };
            _context.Usuarios.Add(usuarioExistente);
            _context.SaveChanges();

            var usuarioNovo = new Usuario
            {
                Nome = "Usuario Novo",
                Email = "existente@teste.com",
                Senha = "SenhaNova123",
                Telefone = "987654321",
                Endereco = "Endereco Novo",
                Cpf = "12345678902"
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () => await _usuarioService.CriarUsuario(usuarioNovo));
        }

        [Test]
        public async Task DeveRetornarUsuarioPorId()
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
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            var usuarioRetornado = await _usuarioService.ConsultaUsuarioPorId(usuario.Id);

            Assert.IsNotNull(usuarioRetornado);
            Assert.AreEqual(usuario.Email, usuarioRetornado.Email);
        }

        [Test]
        public void DeveLancarExcecaoQuandoUsuarioNaoEncontradoPorId()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _usuarioService.ConsultaUsuarioPorId(999));
        }
        
        [Test]
        public void DeveLancarExcecaoQuandoAtualizarUsuarioNaoExistente()
        {
            var usuario = new Usuario
            {
                Id = 999,
                Nome = "Usuario Inexistente",
                Email = "inexistente@teste.com",
                Senha = "SenhaInexistente123",
                Telefone = "987654321",
                Endereco = "Endereco Inexistente",
                Cpf = "12345678903"
            };

            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _usuarioService.AtualizarUsuario(usuario));
        }

        [Test]
        public async Task DeveExcluirUsuarioExistente()
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
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            var resultado = await _usuarioService.ExcluirUsuario(usuario.Id);

            Assert.IsTrue(resultado);
            var usuarioExcluido = _context.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
            Assert.IsNull(usuarioExcluido);
        }

        [Test]
        public void DeveLancarExcecaoQuandoExcluirUsuarioNaoExistente()
        {
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await _usuarioService.ExcluirUsuario(999));
        }
    }
}
