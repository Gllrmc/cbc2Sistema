using Sistema.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Maestros
{
    public class Grpconcepto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int empresaId { get; set; }
        [Required]
        public string orden { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public bool esajuape { get; set; }
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
        public IEnumerable<Conbanco> conbancos { get; set; }
        public IEnumerable<Conconta> concontas { get; set; }
        public IEnumerable<Movimiento> movimientos { get; set; }
    }
}
