using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace ProyectoPersonal_AppVentas.Controllers
{
    public class ClienteController : Controller
    {
        ClienteBL clienteBL = new ClienteBL();

        // GET: Cliente
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var lista = clienteBL.ListarClientes();
            return View(lista);
        }

        [HttpPost]
        public JsonResult Guardar(Cliente cliente)
        {
            bool resultado = true;
            string mensaje = "";

            try
            {
                clienteBL.GestionarCliente(cliente);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje });
        }

        public JsonResult Buscar(string nombre)
        {
            bool resultado = true;
            string mensaje = "";
            List<Cliente> lista = new List<Cliente>();

            try
            {
                lista = clienteBL.BuscarClientes(nombre);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }
            return Json(new { resultado, mensaje, data = lista}, JsonRequestBehavior.AllowGet);
        }

    }
}