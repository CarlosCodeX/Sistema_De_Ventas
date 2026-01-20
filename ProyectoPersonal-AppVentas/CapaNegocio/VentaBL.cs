using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
using CapaEntidad.DTO;

namespace CapaNegocio
{
    public class VentaBL
    {

        VentaDAL ventaDAL = new VentaDAL();

        public int RegistrarVenta(int idCliente, List<DetalleVentaDTO> detalles)
        {
            if (idCliente <= 0)
                throw new Exception("Cliente inválido");

            if (detalles == null || detalles.Count == 0)
                throw new Exception("La venta debe tener productos");

            return ventaDAL.RegistrarVenta(idCliente, detalles);
        }

        public List<Venta> ListarVentasHoy()
        {
            return ventaDAL.listarHoy();
        }

        public List<Venta> ListarVentasMes()
        {
            return ventaDAL.listarMes();
        }

        public List<Venta> ListarVentasAdmin()
        {
            return ventaDAL.listarAdmin();
        }

        public decimal TotalVentasHoy()
        {
            return ventaDAL.TotalVentasHoy();
        }

        public decimal TotalVentasMes()
        {
            return ventaDAL.TotalVentasMes();
        }

        public List<Venta> BuscarVentasAdmin(string nombreCliente)
        {
            if (string.IsNullOrWhiteSpace(nombreCliente))
                return ventaDAL.listarAdmin();

            return ventaDAL.BuscarAdmin(nombreCliente);
        }
        public Venta ObtenerDetalleVenta(int idVenta)
        {
            if (idVenta <= 0)
                throw new Exception("ID de venta inválido");

            return ventaDAL.detalles(idVenta);
        }

    }
}
