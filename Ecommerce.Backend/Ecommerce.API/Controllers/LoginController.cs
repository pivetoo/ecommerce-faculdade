using Ecommerce.Application.DTOs;
using Ecommerce.Dominio.IService;
using Ecommerce.Dominio.Util;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public LoginController(IUsuarioService usuarioService, IConfiguration configuration)
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
