using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLogin.Models;
using WebLogin.Datos;

namespace WebLogin.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel oUsuario = new UsuarioModel();
        UsuarioDatos oUsuarioDatos = new UsuarioDatos();

        public ActionResult Listar()
        {
            var oLista = oUsuarioDatos.Consultar();
            return View(oLista);
        }

     
    }
}
