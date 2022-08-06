using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.webAPI.Contexts;
using Users.webAPI.Domains;
using Users.webAPI.Interfaces;

namespace Users.webAPI.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        UsersContext ctx = new();

        public void Atualizar(Guid idTipoUsuario, TipoUsuario tipoUsuario)
        {
            TipoUsuario tipoUsuarioCadastrado = BuscarPorId(idTipoUsuario);

            tipoUsuarioCadastrado.NomeTipoUsuario = tipoUsuario.NomeTipoUsuario;

            ctx.TipoUsuarios.Update(tipoUsuarioCadastrado);
            ctx.SaveChanges();
        }

        public TipoUsuario BuscarPorId(Guid idTipoUsuario)
        {
            return ctx.TipoUsuarios.Include(tipoUsuario => tipoUsuario.Usuarios).FirstOrDefault(tipoUsuario => tipoUsuario.IdTipoUsuario == idTipoUsuario);
        }

        public void Cadastrar(TipoUsuario novoTipoUsuario)
        {
            ctx.TipoUsuarios.Add(novoTipoUsuario);
            ctx.SaveChanges();
        }

        public void Deletar(Guid idTipoUsuario)
        {
            TipoUsuario tipoUsuarioCadastrado = BuscarPorId(idTipoUsuario);

            ctx.TipoUsuarios.Remove(tipoUsuarioCadastrado);
            ctx.SaveChanges();
        }

        public List<TipoUsuario> ListarTodos()
        {
            return ctx.TipoUsuarios.Include(tipoUsuario => tipoUsuario.Usuarios).ToList();
        }
    }
}
