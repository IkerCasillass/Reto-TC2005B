﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLogin.Datos;
using WebLogin.Models;

namespace WebLogin.Controllers
{
    public class GrupoController : Controller
    {
        GrupoDatos cGrupos = new GrupoDatos();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Listar()
        {
            var listaGrupos = cGrupos.Consultar(); // Reemplaza con tu lógica para obtener todos los Grupos
            return View(listaGrupos);
        }

        public IActionResult Guardar()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Guardar(GrupoModel Grupo)
        {
            if (!ModelState.IsValid)
            {
                return View(Grupo);
            }


            var resp = cGrupos.Guardar(Grupo);
            if (resp)
            {
                Console.WriteLine("Guardado ");
                return RedirectToAction("Listar", "Grupo");
            }
            else
            {
                return View();
            }

            //cGrupos.Guardar(Grupo); // Reemplaza con tu lógica para guardar un Grupo

            //return RedirectToAction("Listar", "Grupo");
        }

        public IActionResult Editar(string nombre_grupo)
        {
            var Grupo = cGrupos.Obtener(nombre_grupo);
            return View(Grupo);
        }

        //[HttpPost]
        //public IActionResult Editar(GrupoModel Grupo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(Grupo);
        //    }

        //    cGrupos.Actualizar(Grupo); // Reemplaza con tu lógica para actualizar un Grupo

        //    return RedirectToAction("Listar");
        //}

        public IActionResult Eliminar(string nombre_grupo)
        {
            var oGrupo = cGrupos.Obtener(nombre_grupo);
            return View(oGrupo);
        }

        [HttpPost]
        public IActionResult Eliminar(GrupoModel oGrupo)
        {
            //if (!ModelState.IsValid)
            //    return View();

            var respuesta = cGrupos.Eliminar(oGrupo.Nombre);

            if (respuesta)
            {
                return RedirectToAction("Listar");
            }
            else
                return View();
        }
    }
}