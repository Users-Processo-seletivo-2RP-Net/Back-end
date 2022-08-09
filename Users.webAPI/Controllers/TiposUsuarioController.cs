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
    public class TiposUsuarioController : ControllerBase
    {
        private ITipoUsuarioRepository _tipoUsuarioRepository { get; set; }

        public TiposUsuarioController(ITipoUsuarioRepository repository)
        {
            _tipoUsuarioRepository = repository;
        }

        [HttpPost]
        public IActionResult Cadastrar(TipoUsuario tipoUsuario)
        {
            try
            {
                if (tipoUsuario.NomeTipoUsuario != null)
                {
                    _tipoUsuarioRepository.Cadastrar(tipoUsuario);
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
                return Ok(_tipoUsuarioRepository.ListarTodos());
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
                TipoUsuario tipoUsuario = _tipoUsuarioRepository.BuscarPorId(id);

                if (tipoUsuario != null)
                {
                    return Ok(tipoUsuario);
                }

                return NotFound("Tipo de usuário não encontrado");
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(Guid id, TipoUsuario tipoUsuario)
        {
            try
            {
                TipoUsuario tipoUsuarioBanco = _tipoUsuarioRepository.BuscarPorId(id);

                if (tipoUsuarioBanco != null)
                {
                    if (tipoUsuario.NomeTipoUsuario != null)
                    {
                        _tipoUsuarioRepository.Atualizar(id, tipoUsuario);
                        return Ok();
                    }
                    return BadRequest("Algumas informações não foram inseridas");
                }

                return NotFound("Tipo de usuário não encontrado");
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
                TipoUsuario tipoUsuario = _tipoUsuarioRepository.BuscarPorId(id);

                if (tipoUsuario != null)
                {
                    _tipoUsuarioRepository.Deletar(id);
                    return Ok();
                }

                return NotFound("O tipo de usuário informado não foi encontrado");
            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }
    }
}
