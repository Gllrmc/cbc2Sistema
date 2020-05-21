using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Concuentas
{
    public class ConcuentaCreateModel
    {
        [Required]
        public int empresaId { get; set; }
        [Required]
        public string apporigen { get; set; }
        [Required]
        public string moneda { get; set; }
        [Required]
        public string numcuenta { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
