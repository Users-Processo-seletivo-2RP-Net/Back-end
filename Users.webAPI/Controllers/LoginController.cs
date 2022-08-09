using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Users.webAPI.Domains;
using Users.webAPI.Interfaces;
using Users.webAPI.ViewModels;

namespace Users.webAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public LoginController(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        [HttpPost]
        public IActionResult Entrar(LoginViewModel login)
        {

            try
            {
                Usuario usuarioBuscado = _usuarioRepository.Login(login.Email, login.Senha);

                if (usuarioBuscado == null)
                {
                    return BadRequest("Usuário não encontrado.");
                }

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.IdUsuario.ToString()),
                    new Claim(ClaimTypes.Role, usuarioBuscado.IdTipoUsuario.ToString()),
                    new Claim("role", usuarioBuscado.IdTipoUsuario.ToString()),
                    new Claim("caminhoImagem", usuarioBuscado.ImagemPerfil.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("users-chave-autenticacao"));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var TokenGerado = new JwtSecurityToken(
                    issuer: "Users.webAPI",
                    audience: "Users.webAPI",
                    claims: claims,
                    expires: DateTime.Now.AddHours(2),
                    signingCredentials: creds
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(TokenGerado)
                });
            }
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
        }
    }
}
