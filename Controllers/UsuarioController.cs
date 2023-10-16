using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebReto.Models;
using WebReto.Datos;

namespace WebReto.Controllers
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
