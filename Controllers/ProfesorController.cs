using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebReto.Datos;
using WebReto.Models;

namespace WebReto.Controllers
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

        public IActionResult Asignaturas(int idProfesor)
        {
            var Profesor = cProfesores.ConsultarAsignaturas(idProfesor);
            return View(Profesor);
        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Guardar(ProfesorModel Profesor)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(Profesor);
            //}
            

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

        }

        public IActionResult NuevaAsignatura()
        {
            return View();
        }

        public IActionResult NuevaAsignatura(ProfesorModel Profesor)
        {

            var resp = cProfesores.GuardarAsignatura(Profesor);
            if (resp)
            {

                return RedirectToAction("Listar", "Profesor");
            }
            else
            {
                return View();
            }

        }


        public IActionResult Editar(int idProfesor)
        {
            var Profesor = cProfesores.Obtener(idProfesor);
            return View(Profesor);
        }

        [HttpPost]
        public IActionResult Editar(ProfesorModel Profesor)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(Profesor);
            //}

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