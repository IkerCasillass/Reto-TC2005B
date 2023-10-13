using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebLogin.Models
{
    public class ClienteModel
    {
        public int IdCliente { get; set; }

        [Required(ErrorMessage="El Nombre del cliente es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage ="El Apellido Paterno es requerido")]
        public string ApPaterno { get; set; }

        [Required(ErrorMessage = "El Apellido Materno es requerido")]
        public string ApMaterno { get; set; }

        [StringLength(2)]
        public int Edad { get; set; }

        [Required(ErrorMessage ="El RFC tambien es requerido")]
        [StringLength(13)]
        public string Rfc { get; set; }
    }
}
