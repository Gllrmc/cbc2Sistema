using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Maestros.Paises
{
    public class PaisUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El país no debe tener mas de 50 caracteres, ni menos de 3 caracteres")]
        public string nombre { get; set; }
        public string cuit { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
    }
}
