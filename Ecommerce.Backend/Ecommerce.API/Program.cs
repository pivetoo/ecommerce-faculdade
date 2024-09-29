using Ecommerce.API.Middlewares;
using Ecommerce.Dominio.IRepository;
using Ecommerce.Dominio.Services;
using Ecommerce.Dominio.Util;
using Ecommerce.Repositorio.Data;
using Ecommerce.Repositorio.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Configuração da Autenticação JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// Adiciona o middleware de tratamento de exceções
builder.Services.AddSingleton<ErrorHandlingMiddleware>();

ConfigureServices(builder.Services, configuration);

var app = builder.Build();

Configure(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddControllers();

    var connectionString = configuration.GetConnectionString("DefaultConnection");
    services.AddDbContext<EcommerceDbContext>(options =>
        options.UseNpgsql(connectionString));

    // Registrar Repositórios
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

    // Habilitar Swagger para API documentation
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // Configuração de CORS
    services.AddCors(b => b.AddDefaultPolicy(c =>
        c.AllowAnyMethod()
         .AllowAnyHeader()
         .AllowAnyOrigin()));
}

void Configure(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseExceptionHandler("/error");
        app.UseHsts();
    }

    // Middleware de tratamento de exceções
    app.UseExceptionHandler(new ExceptionHandlerOptions
    {
        ExceptionHandler = async context =>
        {
            var exceptionHandler = app.Services.GetRequiredService<ErrorHandlingMiddleware>();
            var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (exceptionFeature != null)
            {
                await exceptionHandler.TryHandleAsync(context, exceptionFeature.Error, context.RequestAborted);
            }
        }
    });

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseCors();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints => endpoints.MapControllers());
}
