using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaNegocio;

namespace ProyectoPersonal_AppVentas.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioBL usuarioBL = new UsuarioBL();

        // GET: Usuario
        public ActionResult Index()
        {
            var lista = usuarioBL.ListarUsuario();
            return View(lista);
        }

        [HttpPost]
        public JsonResult Guardar(Usuario usuario)
        {
            bool resultado = true;
            string mensaje = "";

            try
            {
                usuarioBL.GestionarUsuarios(usuario);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CambiarEstado(int id, bool activo)
        {
            bool resultado = true;
            string mensaje = "";

            try
            {
                Usuario usuario = new Usuario
                {
                    IdUsuario = id,
                    Activo = activo,
                };
                usuarioBL.CambiarEstadoUsuario(usuario);
            }
            catch (Exception ex)
            {
                resultado = false;
                mensaje = ex.Message;
            }

            return Json(new { resultado, mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Login (string nombre, string clave)
        {

            Usuario usuario = usuarioBL.LoginUsuario(nombre, clave);

            if (usuario == null)
            {
                ViewBag.Error = "Usuario o Contraseña Incorrectos";
                return View("Login");
            }
            if (!usuario.Activo)
            {
                ViewBag.Error = "Usuario desactivado";
                return View("Login");
            }

            Session["Usuario"] = usuario;
            Session["Rol"] = usuario.rol.NombreRol;

            if (usuario.rol.NombreRol == "Administrador")
            {
                return RedirectToAction("IndexAdministrador", "Usuario");
            }

            if (usuario.rol.NombreRol == "Trabajador")
            {
                return RedirectToAction("Index", "Usuario");
            }

            ViewBag.Error = "Rol no autorizado";
            return View("Login");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


    }
}