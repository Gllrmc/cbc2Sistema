using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.Operaciones
{
    public class Lote
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("empresa")]
        public int empresaId { get; set; }
        [Required]
        [ForeignKey("asocuenta")]
        public int asocuentaId { get; set; }
        [Required]
        public string anio { get; set; }
        [Required]
        public string mes { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal bansalini { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal bansalfin { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal consalini { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal consalfin { get; set; }
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
        public Asocuenta asocuenta { get; set; }
        public ICollection<Movimiento> movimientos { get; set; }
    }
}
