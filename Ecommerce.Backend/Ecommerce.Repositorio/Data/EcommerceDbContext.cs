using Ecommerce.Dominio.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Repositorio.Data
{
    public class EcommerceDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Avaliacao> Avaliacoes { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ChatbotContent> ChatbotContents { get; set; }
        public DbSet<EnderecoEntrega> EnderecoEntregas { get; set; }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<ItemCarrinho> ItensCarrinho { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidosItems { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
