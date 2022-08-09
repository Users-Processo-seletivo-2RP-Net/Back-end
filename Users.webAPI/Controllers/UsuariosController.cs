using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.webAPI.Domains;
using Users.webAPI.Interfaces;

namespace Users.webAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuariosController(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        [Authorize(Roles = "01f6ca89-8f02-43bb-9f2f-b1886321ad00,862f0a4e-fd18-4ca6-a522-3ebe1fc8e5f5")]
        [HttpPost]
        public IActionResult Cadastrar([FromForm] Usuario usuario, IFormFile imagem)
        {
            try
            {
                if (usuario.Email != null && usuario.IdTipoUsuario != null && usuario.Nome != null && usuario.Senha != null && (usuario.StatusUsuario == true || usuario.StatusUsuario == false))
                {
                    _usuarioRepository.Cadastrar(usuario, imagem);
                    return StatusCode(201);
                }

                return BadRequest("Algumas informações não foram fornecidas");
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Authorize(Roles = "01f6ca89-8f02-43bb-9f2f-b1886321ad00,862f0a4e-fd18-4ca6-a522-3ebe1fc8e5f5")]
        [HttpGet]
        public IActionResult ListarTodos()
        {
            try
            {
                return Ok(_usuarioRepository.ListarTodos());
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorId(Guid id)
        {
            try
            {
                Usuario usuario = _usuarioRepository.BuscarPorId(id);

                if (usuario != null)
                {
                    return Ok(usuario);
                }

                return NotFound("Usuário não encontrado");
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromForm] Usuario usuario, IFormFile imagem, Guid id)
        {
            try
            {
                Usuario usuarioBanco = _usuarioRepository.BuscarPorId(id);

                if (usuarioBanco != null)
                {
                    if (usuario.Email != null && usuario.IdTipoUsuario != null && usuario.Nome != null && usuario.Senha != null && (usuario.StatusUsuario == true || usuario.StatusUsuario == false))
                    {
                        _usuarioRepository.Atualizar(id, usuario, imagem);
                        return Ok();
                    }
                    return BadRequest("Algumas informações não foram inseridas");
                }

                return NotFound("Usuário não encontrado");
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Authorize(Roles = "01f6ca89-8f02-43bb-9f2f-b1886321ad00")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(Guid id)
        {
            try
            {
                Usuario usuario = _usuarioRepository.BuscarPorId(id);

                if (usuario != null)
                {
                    _usuarioRepository.Deletar(id);
                    return Ok();
                }

                return NotFound("O usuário informado não foi encontrado");
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
