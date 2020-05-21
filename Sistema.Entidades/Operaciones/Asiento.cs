using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.Operaciones
{
    public class Asiento
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("empresa")]
        public int empresaId { get; set; }
        public string comentario { get; set; }
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
        public Empresa empresa { get; set; }
        public ICollection<Movimiento> movimientos { get; set; }
    }
}
