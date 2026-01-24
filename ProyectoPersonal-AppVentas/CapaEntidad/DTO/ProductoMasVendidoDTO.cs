using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTO
{
    public class ProductoMasVendidoDTO
    {

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int TotalVendido { get; set; }

    }
}
