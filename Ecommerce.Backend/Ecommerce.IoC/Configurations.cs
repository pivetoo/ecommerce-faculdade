using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.Services;
using Ecommerce.Dominio.Util;
using Ecommerce.Repositorio.Data;
using Ecommerce.Repositorio.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.IoC
{
    public static class Configurations
    {
        public static IServiceCollection AddInjections(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<EcommerceDbContext>(options =>
                    options.UseNpgsql(connectionString));

            services.AddScoped<ChatbotOpenAI>();

            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<AvaliacaoService>();

            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<CarrinhoService>();

            services.AddScoped<PagamentoService>();

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<PedidoService>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ProdutoService>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<UsuarioService>();

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<CategoriaService>();

            services.AddScoped<IFaqRepository, FaqRepository>();
            services.AddScoped<FaqService>();

            services.AddScoped<IChatbotContentRepository, ChatbotContentRepository>();
            services.AddScoped<ChatbotContentService>();

            return services;
        }
    }
}
