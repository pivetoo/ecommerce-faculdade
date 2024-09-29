using Ecommerce.Dominio.Entities;
using Ecommerce.Dominio.Services;
using Ecommerce.Dominio.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public UsuarioController(UsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CriarUsuario(Usuario usuario)
        {
            await _usuarioService.CriarUsuario(usuario);
            return Ok("Usuário criado com sucesso!");
        }

        [HttpPost("solicitar-reset-senha")]
        public async Task<IActionResult> SolicitarResetSenha(string email)
        {
            var usuario = await _usuarioService.ConsultaUsuarioPorEmail(email);
            var token = JwtToken.GerarTokenResetSenha(usuario.Id.ToString(), usuario.Email, _configuration);

            var linkDeReset = Url.Action(nameof(ResetarSenha), "Usuario", new { token }, Request.Scheme);
            EmailTools.EnviarEmailResetSenha(usuario.Email, linkDeReset);

            return Ok("Link de recuperação de senha enviado para o e-mail");
        }

        [HttpGet("resetar-senha")]
        public IActionResult ResetarSenha(string token)
        {
            var claimsPrincipal = JwtToken.ValidarToken(token, _configuration);
            if (claimsPrincipal == null)
            {
                return BadRequest("Token inválido ou expirado.");
            }

            return Ok("Token válido. Redefina sua senha.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> ObterUsuario(long id)
        {
            var usuario = await _usuarioService.ConsultaUsuarioPorId(id);
            return Ok(usuario);
        }

        [HttpGet("atualizar-senha")]
        public async Task<IActionResult> AtualizarUsuario(Usuario usuario)
        {
            await _usuarioService.AtualizarUsuario(usuario);
            return Ok("Usuário atualizado com sucesso!");
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirUsuario(int id)
        {
            await _usuarioService.ExcluirUsuario(id);
            return Ok("Usuário excluído com sucesso!");
        }

        [HttpGet("count")]
        public async Task<IActionResult> ContarUsuarios()
        {
            var totalUsuarios = await _usuarioService.ContarUsuarios();
            return Ok(new { total = totalUsuarios });
        }
    }
}
