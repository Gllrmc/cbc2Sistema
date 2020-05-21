using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Operaciones
{
    public class LoteUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int empresaId { get; set; }
        [Required]
        public int asocuentaId { get; set; }
        [Required]
        public string anio { get; set; }
        [Required]
        public string mes { get; set; }
        [Required]
        public decimal bansalini { get; set; }
        [Required]
        public decimal bansalfin { get; set; }
        [Required]
        public decimal consalini { get; set; }
        [Required]
        public decimal consalfin { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
