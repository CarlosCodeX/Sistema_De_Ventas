using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Categoria
    {

        public int IdCategoria { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public bool Activo { get; set; }

        public Categoria(int idCategoria, string nombre, string descripcion, bool activo)
        {
            IdCategoria = idCategoria;
            Nombre = nombre;
            Descripcion = descripcion;
            Activo = activo;
        }

        public Categoria()
        {
        }
    }
}
