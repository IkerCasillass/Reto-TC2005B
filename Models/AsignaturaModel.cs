using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebReto.Models
{
    public class AsignaturaModel
    {

        public int IdAsignatura { get; set; }

        public string Nombre { get; set; }

        public string Grupo { get; set; }
    }
}
