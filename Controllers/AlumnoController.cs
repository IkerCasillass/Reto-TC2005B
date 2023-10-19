using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebReto.Datos;
using WebReto.Models;
using X.PagedList;

namespace WebReto.Controllers
{
    public class AlumnoController : Controller
    {
        AlumnoDatos cAlumnos = new AlumnoDatos();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar(int? pagina)
        {
            int elementosPorPagina = 20;
            int numeroPagina = (pagina ?? 1);
            int offset = (numeroPagina - 1) * elementosPorPagina;

            var listaAlumnos = cAlumnos.ConsultarPaginado(elementosPorPagina, offset);

            IPagedList<AlumnoModel> alumnosPaginados = listaAlumnos.ToPagedList(numeroPagina, elementosPorPagina);

            return View(alumnosPaginados);
        }

        public IActionResult ListarRanking(int? pagina)
        {
            int elementosPorPagina = 20;
            int numeroPagina = (pagina ?? 1);
            int offset = (numeroPagina - 1) * elementosPorPagina;

            var listaAlumnos = cAlumnos.ConsultarPaginadoRanking(elementosPorPagina, offset);

            IPagedList<AlumnoModel> alumnosPaginados = listaAlumnos.ToPagedList(numeroPagina, elementosPorPagina);

            return View(alumnosPaginados);
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

            cAlumnos.Actualizar(alumno); 

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