using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.DTO
{
    public class VentaDTO
    {
        public int IdCliente { get; set; }
        public List<DetalleVentaDTO> Detalles { get; set; }
    }
}
