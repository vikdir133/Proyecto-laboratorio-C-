using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiBotica.SolPedido.Entidades.Core;
using MiBotica.SolPedido.LogicaNegocio.Core;
using MiBotica.SolPedido.Utiles.Helpers;

namespace MiBotica.SolPedido.Cliente.Web.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: /Usuario
        public ActionResult Index()
        {
                List<Usuario> usuario = new List<Usuario>();
                usuario = new UsuarioLN().ListaUsuarios(); // llama a LN
                return View(usuario);
        }
        // GET : /Usuario/Create
        public ActionResult Create()
        {
            Usuario usuario = new Usuario();
            return View(usuario);
        }

        // POST: /Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Usuario usuario)
        {
            try
            {
                // Encriptar la clave en texto antes de grabar
                usuario.Clave = EncriptacionHelper.EncriptarByte(usuario.ClaveTexto ?? string.Empty);

                // Invocar capa de negocio (aún no existe, lo creas en el paso 2)
                new UsuarioLN().InsertarUsuario(usuario);

                return RedirectToAction("Index");
            }
            catch
            {
                // Si falla, vuelve a mostrar el formulario
                return View(usuario);
            }
        }
    }
}