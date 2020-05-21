using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Asocuentas
{
    public class AsocuentaCreateModel
    {
        [Required]
        public int empresaId { get; set; }
        [Required]
        public string orden { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        public int bancuentaId { get; set; }
        [Required]
        public int concuentaId { get; set; }
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
