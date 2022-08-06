using System;
using System.Collections.Generic;

#nullable disable

namespace Users.webAPI.Domains
{
    public partial class Usuario
    {
        public Guid IdUsuario { get; set; }
        public Guid? IdTipoUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool StatusUsuario { get; set; }
        public string ImagemPerfil { get; set; }

        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
    }
}
