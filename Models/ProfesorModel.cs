using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;

namespace WebReto.Models
{
    public class ProfesorModel
    {
        public int IdProfesor { get; set; }

        public string Nombre { get; set; }

        public string ApPaterno { get; set; }

        public string ApMaterno { get; set; }

        public int Edad { get; set; }

        public string Telefono { get; set; }
        public string Email { get; set; }

        public List<AsignaturaModel> Asignaturas { get; set; } // Lista de asignaturas que imparte

        public string AsignaturaActual { get; set; }
    }
}
