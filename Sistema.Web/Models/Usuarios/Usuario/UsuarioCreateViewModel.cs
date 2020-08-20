using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Usuarios.Usuario
{
    public class UsuarioCreateViewModel
    {
        [Required]
        public int rolId { get; set; }
        public int? personaId { get; set; }
        [Required]
        public string userid { get; set; }
        public string telefono { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public bool pxch { get; set; }
        [Required]
        public int iduseralta { get; set; }

    }
}
