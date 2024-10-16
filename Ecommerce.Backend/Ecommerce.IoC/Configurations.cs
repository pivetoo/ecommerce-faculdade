﻿using Ecommerce.Application.Services;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.IService;
using Ecommerce.Dominio.Util;
using Ecommerce.Repositorio.Data;
using Ecommerce.Repositorio.Repositories;
using EEcommerce.Application.Services;
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
            services.AddScoped<IAvaliacaoService, AvaliacaoService>();

            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();

            services.AddScoped<PagamentoService>();

            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoService, PedidoService>();

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
