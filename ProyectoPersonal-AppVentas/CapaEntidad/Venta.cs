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
        public decimal Total { get; set; }
        public List<DetalleVenta> Detalles { get; set; }

        public Venta(int idVenta, Cliente cliente, DateTime fechaVenta, decimal total, List<DetalleVenta> detalles)
        {
            IdVenta = idVenta;
            this.cliente = cliente;
            FechaVenta = fechaVenta;
            Total = total;
            Detalles = detalles;
        }

        public Venta()
        {
        }
    }
}
