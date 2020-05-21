using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Personas
{
    public class PersonaCreateModel
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        public string domicilio { get; set; }
        public string localidad { get; set; }
        [StringLength(50, ErrorMessage = "El codigo postal no debe de tener más de 8 caracteres.")]
        public string cpostal { get; set; }
        [Required]
        public int provinciaId { get; set; }
        [Required]
        public int paisId { get; set; }
        [EmailAddress]
        public string emailpersonal { get; set; }
        public string telefonopersonal { get; set; }
        public string tipodocumento { get; set; }
        public string numdocumento { get; set; }
        [Required]
        public bool esempleado { get; set; }
        [Required]
        public bool esproveedor { get; set; }
        [Required]
        public bool escliente { get; set; }
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
