using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class UsuarioBL
    {

        UsuarioDAL usuarioDAL = new UsuarioDAL();

        public int GestionarUsuarios (Usuario usuario)
        {
            if(usuario == null)
                throw new Exception("Usuario inválida");

            if (usuario.IdUsuario < 0)
                throw new Exception("ID inválido");

            if (usuario.IdUsuario == 0)
                return usuarioDAL.agregar(usuario);
            else
                return usuarioDAL.actualizar(usuario);
        }

        public int CambiarEstadoUsuario (Usuario usuario)
        {
            if (usuario == null)
                throw new Exception("Categoría inválida");

            if (usuario.IdUsuario <= 0)
                throw new Exception("ID inválido");

            if (usuario.Activo)
                return usuarioDAL.desactivar(usuario.IdUsuario);
            else
                return usuarioDAL.reactivar(usuario.IdUsuario);
        }

        public List<Usuario> ListarUsuario()
        {
            return usuarioDAL.listar();
        }

        public Usuario LoginUsuario (string nombre, string clave)
        {
            return usuarioDAL.login(nombre, clave);
        }

    }
}
