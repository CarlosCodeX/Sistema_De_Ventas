using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace ProyectoPersonal_AppVentas.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriaBL categoriaBL = new CategoriaBL();

        // GET: Categoria
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Login", "Usuario");
            }

            var lista = categoriaBL.ListarCategoriaSinFiltro();
            return View(lista);
        }

        [HttpPost]
        public JsonResult Guardar(Categoria categoria)
        {
            bool resultado = true;
            string mensaje = "";

            try
            {
                categoriaBL.GestionarCategoria(categoria);
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
                Categoria categoria = new Categoria
                {
                    IdCategoria = id,
                    Activo = activo
                };

                categoriaBL.CambiarEstadoCategoria(categoria);
            }
            catch(Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }
    }
}