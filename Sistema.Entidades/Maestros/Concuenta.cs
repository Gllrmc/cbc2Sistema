using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.Maestros
{
    public class Concuenta
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [ForeignKey("empresa")]
        [Required]
        public int empresaId { get; set; }
        [Required]
        public string apporigen { get; set; }
        [Required]
        public string moneda { get; set; }
        [Required]
        public string numcuenta { get; set; }
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
        public IEnumerable<Asocuenta> asocuentas { get; set; }
    }
}
