using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Producto
    {

        public int IdProducto { get; set; }
        public String Nombre { get; set; }
        public Categoria categoria { get; set; } 
        public int Stock { get; set; }
        public double Precio { get; set; }
        public DateTime FechaRegistro { get; set; }
        public bool Activo { get; set; }

    }
}
