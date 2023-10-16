using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReto.Models
{
    public class GrupoModel
    {
        public string Nombre { get; set; }

        public int Grado { get; set; }

        public int NumEstudiantes { get; set; }

        public List<AlumnoModel> Estudiantes { get; set; }

        public List<ProfesorModel> Profesores { get; set; }

    }
}
