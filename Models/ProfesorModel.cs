using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLogin.Models
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
    }
}
