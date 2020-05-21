using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Bancos
{
    public class BancoCreateModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La empresa no debe tener mas de 50 caracteres, ni menos de 3 caracteres")]
        public string nombre { get; set; }
        [Required]
        public string nombrecorto { get; set; }
        [Required]
        public string codigoBCRA { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
