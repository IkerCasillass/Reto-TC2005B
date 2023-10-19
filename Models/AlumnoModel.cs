using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReto.Models
{
    public class AlumnoModel
    {
        public int IdAlumno { get; set; }

        public string Nombre { get; set; }

        public string ApPaterno { get; set; }

        public string ApMaterno { get; set; }

        public int Grado { get; set; }

        [ValidateNever]
        public string Grupo { get; set; }

        public int Edad { get; set; }

        public int Passwd { get; set; }

        public int Estatus { get; set; }

        public int Puntaje { get; set; }
    }
}
