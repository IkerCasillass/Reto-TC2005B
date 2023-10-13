using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLogin.Datos;
using WebLogin.Models;

namespace WebLogin.Controllers
{
    public class ClienteController : Controller
    {
        ClienteDatos cdatos = new ClienteDatos();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar()
        {
            var oListarClientes = cdatos.Consultar();
            return View(oListarClientes);
        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Guardar(ClienteModel oC)
        {
            if (!ModelState.IsValid)
                return View();

            var resp = cdatos.Guardar(oC);
            if (resp)
            {
                return RedirectToAction("Listar", "Cliente");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Editar(int idCliente)
        {
            var oCliente = cdatos.Obtener(idCliente);
            return View(oCliente);
        }

        [HttpPost]
        public IActionResult Editar(ClienteModel oCliente)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = cdatos.Actualizar(oCliente);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
                return View();
        }

        public IActionResult Eliminar(int idCliente)
        {
            var oCliente = cdatos.Obtener(idCliente);
            return View(oCliente);
        }

        [HttpPost]
        public IActionResult Eliminar(ClienteModel oCliente)
        {
            if (!ModelState.IsValid)
                return View();

            var respuesta = cdatos.Eliminar(oCliente.IdCliente);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
                return View();
        }
    }
}
