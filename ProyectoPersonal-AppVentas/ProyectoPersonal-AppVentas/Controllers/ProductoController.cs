using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;

namespace ProyectoPersonal_AppVentas.Controllers
{
    public class ProductoController : Controller
    {
        ProductoBL productoBL = new ProductoBL();

        // GET: Producto
        public ActionResult Index()
        {
            var lista = productoBL.ListarProducto();
            return View(lista);
        }

        [HttpPost]
        public JsonResult Guardar(Producto producto)
        {
            bool resultado = true;
            string mensaje = "";

            try
            {
                productoBL.GestionarProducto(producto);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CambiarEstado(int id, bool activo)
        {
            bool resultado = true;
            string mensaje = "";

            try
            {
                Producto producto = new Producto
                {
                    IdProducto = id,
                    Activo = activo,
                };

                productoBL.CambiarEstadoProducto(producto);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Buscar(string nombre)
        {
            bool resultado = true;
            string mensaje = "";
            List<Producto> lista = new List<Producto>();

            try
            {
                lista = productoBL.buscarProducto(nombre);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje, data = lista }, JsonRequestBehavior.AllowGet);
        }
    }
}