using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {

        public int IdUsuario { get; set; }
        public String NombreUsuario { get; set; }
        public String Clave { get; set; }
        public Rol rol { get; set; }
        public bool Activo { get; set; }

        public Usuario(int idUsuario, string nombreUsuario, Rol rol, bool activo)
        {
            IdUsuario = idUsuario;
            NombreUsuario = nombreUsuario;
            this.rol = rol;
            Activo = activo;
        }

        public Usuario()
        {
        }
    }
}
