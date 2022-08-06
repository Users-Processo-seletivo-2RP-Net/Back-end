using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Users.webAPI.Domains;

namespace Users.webAPI.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        /// <summary>
        /// Cadastra um novo tipo de usuário
        /// </summary>
        /// <param name="novoTipoUsuario">Objeto tipoUsuario a ser cadastrado</param>
        void Cadastrar(TipoUsuario novoTipoUsuario);

        /// <summary>
        /// Lista um tipo de usuário em específico
        /// </summary>
        /// <param name="idTipoUsuario">ID do tipo de usuário a ser procurado</param>
        /// <returns>Objeto tipoUsuario encontrado</returns>
        TipoUsuario BuscarPorId(Guid idTipoUsuario);

        /// <summary>
        /// Lista todos os tipos de usuário cadastrados
        /// </summary>
        /// <returns>Uma lista de tipos de usuários</returns>
        List<TipoUsuario> ListarTodos();

        /// <summary>
        /// Atualiza um tipo de usuário em específico
        /// </summary>
        /// <param name="idTipoUsuario">ID do tipo de usuário a ser atualizado</param>
        /// <param name="tipoUsuario">Objeto tipoUsuario contendo as novas informações</param>
        void Atualizar(Guid idTipoUsuario, TipoUsuario tipoUsuario);

        /// <summary>
        /// Deleta um tipo de usuário em específico
        /// </summary>
        /// <param name="idTipoUsuario">ID do tipo de usuário a ser deletado</param>
        void Deletar(Guid idTipoUsuario);
    }
}
