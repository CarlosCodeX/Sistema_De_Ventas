using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTO
{
    public class ProductoBajoStockDTO
    {

        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Stock { get; set; }
    }
}
