using Ecommerce.API.DTOs;
using Ecommerce.Dominio.Services;
using Ecommerce.Dominio.Util;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public LoginController(UsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            var usuario = await _usuarioService.ConsultaUsuarioPorEmail(loginDto.Email);
            if (usuario == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            var token = JwtToken.GerarToken(usuario, _configuration);
            var isAdmin = usuario.IsAdmin;

            return Ok(new { token, isAdmin });
        }
    }
}
