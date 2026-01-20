using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;
using CapaEntidad.DTO;

namespace ProyectoPersonal_AppVentas.Controllers
{
    public class VentaController : Controller
    {
        VentaBL ventaBL = new VentaBL();

        // GET: Venta
        public ActionResult Index()
        {
            var lista = ventaBL.ListarVentasAdmin();
            return View(lista);
        }

        [HttpPost]
        public JsonResult RegistrarVenta(VentaDTO venta)
        {
            bool resultado = true;
            string mensaje = "";

            try
            {
                ventaBL.RegistrarVenta(venta.IdCliente, venta.Detalles);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje });
        }


        public JsonResult ListarVentasHoy()
        {
            bool resultado = true;
            string mensaje = "";
            List<Venta> lista = new List<Venta>();

            try
            {
                lista = ventaBL.ListarVentasHoy();
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje, data = lista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarVentasMes()
        {
            bool resultado = true;
            string mensaje = "";
            List<Venta> lista = new List<Venta>();

            try
            {
                lista = ventaBL.ListarVentasMes();
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje, data = lista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListarVentasAdmin()
        {
            bool resultado = true;
            string mensaje = "";
            List<Venta> lista = new List<Venta>();

            try
            {
                lista = ventaBL.ListarVentasAdmin();
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje, data = lista }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalVentasHoy()
        {
            bool resultado = true;
            string mensaje = "";
            decimal ventas = 0;

            try
            {
                ventas = ventaBL.TotalVentasHoy();
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje, ventas }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TotalVentasMes()
        {
            bool resultado = true;
            string mensaje = "";
            decimal ventas = 0;

            try
            {
                ventas = ventaBL.TotalVentasMes();
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje, ventas }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscarVenta(string nombreCliente)
        {
            bool resultado = true;
            string mensaje = "";
            List<Venta> lista = new List<Venta>();

            try
            {
                lista = ventaBL.BuscarVentasAdmin(nombreCliente);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje, data = lista }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalle (int id)
        {
            Venta venta = new Venta();
            venta = ventaBL.ObtenerDetalleVenta(id);

            return View(venta);
        }

    }
}