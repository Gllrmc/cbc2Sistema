using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Usuarios.Usuario
{
    public class UsuarioUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int rolId { get; set; }
        public int? personaId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de tener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string userid { get; set; }
        public string telefono { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public bool act_password { get; set; }
        [Required]
        public bool pxch { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
    }
}
