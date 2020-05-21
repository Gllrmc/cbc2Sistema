using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public int rolId { get; set; }
        public int? personaId { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El userid no debe de tener más de 100, ni menos de 3 caracteres.")]
        public string userid { get; set; }
        public string telefono { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public byte[] password_hash { get; set; }
        [Required]
        public byte[] password_salt { get; set; }
        public int  iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }

        public Rol rol { get; set; }
    }
}
