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
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository { get; set; }

        public UsuariosController(IUsuarioRepository repository)
        {
            _usuarioRepository = repository;
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Usuario usuario, IFormFile imagem)
        {
            try
            {
                if (usuario.Email != null && usuario.IdTipoUsuario != null && usuario.Nome != null && usuario.Senha != null && (usuario.StatusUsuario == true || usuario.StatusUsuario == false) && imagem != null)
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
        public IActionResult Atualizar([FromForm] Guid id, Usuario usuario, IFormFile imagem)
        {
            try
            {
                Usuario usuarioBanco = _usuarioRepository.BuscarPorId(id);

                if (usuarioBanco != null)
                {
                    if (usuario.Email != null && usuario.IdTipoUsuario != null && usuario.Nome != null && usuario.Senha != null && (usuario.StatusUsuario == true || usuario.StatusUsuario == false) && imagem != null)
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
