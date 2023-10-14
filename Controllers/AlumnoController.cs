using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLogin.Datos;
using WebLogin.Models;

namespace WebLogin.Controllers
{
    public class AlumnoController : Controller
    {
        AlumnoDatos cAlumnos = new AlumnoDatos();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar()
        {
            var listaAlumnos = cAlumnos.Consultar(); // Reemplaza con tu lógica para obtener todos los alumnos
            return View(listaAlumnos);
        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Guardar(AlumnoModel alumno)
        {
            if (!ModelState.IsValid)
            {
                return View(alumno);
            }


            var resp = cAlumnos.Guardar(alumno);
            if (resp)
            {
                Console.WriteLine("Guardado ");
                return RedirectToAction("Listar", "Alumno");
            }
            else
            {
                return View();
            }

            //cAlumnos.Guardar(alumno); // Reemplaza con tu lógica para guardar un alumno

            //return RedirectToAction("Listar", "Alumno");
        }

        public IActionResult Editar(int idAlumno)
        {
            var alumno = cAlumnos.Obtener(idAlumno);
            return View(alumno);
        }

        [HttpPost]
        public IActionResult Editar(AlumnoModel alumno)
        {
            if (!ModelState.IsValid)
            {
                return View(alumno);
            }

            cAlumnos.Actualizar(alumno); // Reemplaza con tu lógica para actualizar un alumno

            return RedirectToAction("Listar");
        }

        public IActionResult Eliminar(int idAlumno)
        {
            var oAlumno = cAlumnos.Obtener(idAlumno);
            return View(oAlumno);
        }

        [HttpPost]
        public IActionResult Eliminar(AlumnoModel oAlumno)
        {
            //if (!ModelState.IsValid)
            //    return View();

            var respuesta = cAlumnos.Eliminar(oAlumno.IdAlumno);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
                return View();
        }
    }
}