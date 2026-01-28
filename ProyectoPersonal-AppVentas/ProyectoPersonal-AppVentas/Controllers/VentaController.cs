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
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            return View();
        }

        [HttpGet]
        public ActionResult RegistroVentas()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            ViewBag.Clientes = new ClienteBL().ListarClientes();
            ViewBag.Productos = new ProductoBL().ListarProducto();

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

        [HttpGet]
        public JsonResult ListarVentasHoy()
        {
            var lista = ventaBL.ListarVentasHoy();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarVentasMes()
        {
            var lista = ventaBL.ListarVentasMes();
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarVentasAdmin()
        {
            var lista = ventaBL.ListarVentasAdmin();
            return Json(lista, JsonRequestBehavior.AllowGet);
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
            var lista = ventaBL.BuscarVentasAdmin(nombreCliente);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Detalle (int id)
        {
            Venta venta = new Venta();
            venta = ventaBL.ObtenerDetalleVenta(id);

            return View(venta);
        }

    }
}