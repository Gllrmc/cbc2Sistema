using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Maestros.Provincias
{
    public class ProvinciaCreateModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "La provincia no debe tener mas de 50 caracteres, ni menos de 3 caracteres")]
        public string nombre { get; set; }
        [Required]
        public int paisId { get; set; }
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
