using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {

        public int IdDetalleVenta { get; set; }
        public Venta venta { get; set; }
        public Producto producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }

        public DetalleVenta(Producto producto, int cantidad, decimal precioUnitario, decimal subTotal)
        {
            this.producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            SubTotal = subTotal;
        }

        public DetalleVenta()
        {
        }
    }
}
