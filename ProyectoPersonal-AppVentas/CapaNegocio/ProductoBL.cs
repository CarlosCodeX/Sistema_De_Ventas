using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class ProductoBL
    {

        ProductoDAL productoDAL = new ProductoDAL();

        public int GestionarProducto (Producto producto)
        {
            if (producto == null)
                throw new Exception("Producto inválida");

            if (producto.IdProducto < 0)
                throw new Exception("ID inválido");

            if (producto.IdProducto == 0)
                return productoDAL.agregar(producto);
            else
                return productoDAL.actualizar(producto);
        }

        public int CambiarEstado(Producto producto)
        {
            if (producto == null)
                throw new Exception("Categoría inválida");

            if (producto.IdProducto <= 0)
                throw new Exception("ID inválido");

            if (producto.Activo)
                return productoDAL.desactivar(producto.IdProducto);
            else
                return productoDAL.reactivar(producto.IdProducto);
        }

        public List<Producto> ListarProducto()
        {
            return productoDAL.listar();
        }

        public List<Producto> buscarProducto(string nombre)
        {
            return productoDAL.buscar(nombre);
        }

    }
}
