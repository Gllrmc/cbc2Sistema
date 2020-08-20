using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Grpconceptos
{
    public class GrpconceptoCreateModel
    {
        [Required]
        public int empresaId { get; set; }
        [Required]
        public string orden { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public bool esajuape { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
        [Required]
        public bool activo { get; set; }
    }
}
