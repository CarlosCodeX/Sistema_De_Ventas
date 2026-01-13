using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Venta
    {

        public int IdVenta { get; set; }
        public Cliente cliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public double Total { get; set; }

    }
}
