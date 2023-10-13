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

        //public IActionResult Editar(int id)
        //{
        //    var alumno = cAlumnos.ObtenerPorId(id); // Reemplaza con tu lógica para obtener un alumno por su ID
        //    return View(alumno);
        //}

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

        public IActionResult Eliminar(int id)
        {
            // Realiza la eliminación del alumno
            var respuesta = cAlumnos.Eliminar(id);

            if (respuesta)
            {
                // La eliminación fue exitosa, redirige a la vista con un objeto AlumnoModel
                var oAlumno = cAlumnos.Obtener(id); // Puedes cargar los datos del alumno eliminado si lo necesitas
                return View(oAlumno);
            }
            else
            {
                // La eliminación falló, redirige a una vista de error
                return RedirectToAction("Error");
            }
        }

        //[HttpPost]
        //public IActionResult Eliminar(int id)
        //{
        //    cAlumnos.Eliminar(id); // Reemplaza con tu lógica para eliminar un alumno

        //    return RedirectToAction("Listar");
        //}
    }
}