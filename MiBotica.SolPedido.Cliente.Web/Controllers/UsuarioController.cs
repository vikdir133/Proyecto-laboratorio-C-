using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiBotica.SolPedido.Entidades.Core;
using MiBotica.SolPedido.LogicaNegocio.Core;

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
    }
}