using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.webAPI.Domains;

namespace Users.webAPI.Interfaces
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="novoUsuario">Objeto Usuario a ser cadastrado</param>
        /// <param name="imagemPerfil">Imagem de perfil do usuário</param>
        void Cadastrar(Usuario novoUsuario, IFormFile imagemPerfil);

        /// <summary>
        /// Lista um usuário em específico
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        Usuario BuscarPorId(Guid idUsuario);

        /// <summary>
        /// Lista todos os usuários cadastrados
        /// </summary>
        /// <returns>Uma lista de usuários</returns>
        List<Usuario> ListarTodos();

        /// <summary>
        /// Atualiza um usuário em específico
        /// </summary>
        /// <param name="idUsuario">ID do usuário a ser atualizado</param>
        /// <param name="usuario">Objeto Usuario contendo as novas informações</param>
        /// <param name="imagemPerfil">Nova imagem de perfil do usuário</param>
        void Atualizar(Guid idUsuario, Usuario usuario, IFormFile imagemPerfil);

        /// <summary>
        /// Deleta um usuário em específico
        /// </summary>
        /// <param name="idUsuario">ID do usuário a ser deletado</param>
        void Deletar(Guid idUsuario);

        /// <summary>
        /// Realiza o login do usuário
        /// </summary>
        /// <param name="email">E-mail do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>Objeto Usuario referente as credencias fornecidas</returns>
        Usuario Login(string email, string senha);
    }
}
