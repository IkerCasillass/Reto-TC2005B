using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLogin.Datos;
using WebLogin.Models;

namespace WebLogin.Controllers
{
    public class ProfesorController : Controller
    {
        ProfesorDatos cProfesores = new ProfesorDatos();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar()
        {
            var listaProfesores = cProfesores.Consultar(); // Reemplaza con tu lógica para obtener todos los Profesores
            return View(listaProfesores);
        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Guardar(ProfesorModel Profesor)
        {
            if (!ModelState.IsValid)
            {
                return View(Profesor);
            }


            var resp = cProfesores.Guardar(Profesor);
            if (resp)
            {
                Console.WriteLine("Guardado ");
                return RedirectToAction("Listar", "Profesor");
            }
            else
            {
                return View();
            }

            //cProfesores.Guardar(Profesor); // Reemplaza con tu lógica para guardar un Profesor

            //return RedirectToAction("Listar", "Profesor");
        }

        public IActionResult Editar(int idProfesor)
        {
            var Profesor = cProfesores.Obtener(idProfesor);
            return View(Profesor);
        }

        [HttpPost]
        public IActionResult Editar(ProfesorModel Profesor)
        {
            if (!ModelState.IsValid)
            {
                return View(Profesor);
            }

            cProfesores.Actualizar(Profesor); // Reemplaza con tu lógica para actualizar un Profesor

            return RedirectToAction("Listar");
        }

        public IActionResult Eliminar(int idProfesor)
        {
            var oProfesor = cProfesores.Obtener(idProfesor);
            return View(oProfesor);
        }

        [HttpPost]
        public IActionResult Eliminar(ProfesorModel oProfesor)
        {
            //if (!ModelState.IsValid)
            //    return View();

            var respuesta = cProfesores.Eliminar(oProfesor.IdProfesor);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
                return View();
        }
    }
}