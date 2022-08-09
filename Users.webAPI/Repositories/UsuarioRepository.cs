using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.webAPI.Contexts;
using Users.webAPI.Domains;
using Users.webAPI.Interfaces;
using Users.webAPI.Utils;

namespace Users.webAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        UsersContext ctx = new();

        public void Atualizar(Guid idUsuario, Usuario usuario, IFormFile imagemPerfil)
        {
            Usuario usuarioCadastrado = BuscarPorId(idUsuario);

            usuarioCadastrado.Email = usuario.Email;
            usuarioCadastrado.IdTipoUsuario = usuario.IdTipoUsuario;
            usuarioCadastrado.Nome = usuario.Nome;
            usuarioCadastrado.StatusUsuario = usuario.StatusUsuario;

            if (imagemPerfil.FileName != usuarioCadastrado.ImagemPerfil)
            {
                Upload.RemoverArquivo(usuarioCadastrado.ImagemPerfil);

                string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
                string uploadResultado = Upload.UploadFile(imagemPerfil, extensoesPermitidas);

                usuarioCadastrado.ImagemPerfil = uploadResultado;
            }

            if (Criptografia.Comparar(usuarioCadastrado.Senha, Criptografia.GerarHash(usuario.Senha)) == false)
            {
                usuarioCadastrado.Senha = Criptografia.GerarHash(usuario.Senha);
            }

            ctx.Usuarios.Update(usuarioCadastrado);
            ctx.SaveChanges();
        }

        public Usuario BuscarPorId(Guid idUsuario)
        {
            return ctx.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.IdUsuario == idUsuario);
        }

        public void Cadastrar(Usuario novoUsuario, IFormFile imagemPerfil)
        {
            novoUsuario.Senha = Criptografia.GerarHash(novoUsuario.Senha);

            if (imagemPerfil != null)
            {
                string[] extensoesPermitidas = { "jpg", "png", "jpeg", "gif" };
                string uploadResultado = Upload.UploadFile(imagemPerfil, extensoesPermitidas);

                novoUsuario.ImagemPerfil = uploadResultado;
            }
            else
            {
                novoUsuario.ImagemPerfil = "perfilSemFoto.png";
            }

            ctx.Usuarios.Add(novoUsuario);
            ctx.SaveChanges();
        }

        public void Deletar(Guid idUsuario)
        {
            Usuario usuarioCadastrado = BuscarPorId(idUsuario);

            ctx.Usuarios.Remove(usuarioCadastrado);
            ctx.SaveChanges();
        }

        public List<Usuario> ListarTodos()
        {
            return ctx.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).ToList();
        }

        public Usuario Login(string email, string senha)
        {
            Usuario usuarioEncontrado = ctx.Usuarios.FirstOrDefault(usuario => usuario.Email == email);

            if (usuarioEncontrado != null)
            {
                if (Criptografia.Comparar(senha, usuarioEncontrado.Senha))
                {
                    return usuarioEncontrado;
                }
            }
            return null;
        }
    }
}
