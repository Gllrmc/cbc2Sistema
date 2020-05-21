using System;
using System.ComponentModel.DataAnnotations;


namespace Sistema.Web.Models.Maestros.Personas
{
    public class PersonaUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        public string domicilio { get; set; }
        public string localidad { get; set; }
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
    }
}
