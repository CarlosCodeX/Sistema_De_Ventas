using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTO
{
    public class VentaListadoDTO
    {
        public int IdVenta { get; set; }
        public Cliente cliente { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
    }
}
