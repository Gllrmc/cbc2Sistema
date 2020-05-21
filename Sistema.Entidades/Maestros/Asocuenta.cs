using Sistema.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.Maestros
{
    public class Asocuenta
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("empresa")]
        public int empresaId { get; set; }
        [Required]
        public string orden { get; set; }
        [Required]
        public string descripcion { get; set; }
        [Required]
        [ForeignKey("bancuenta")]
        public int bancuentaId { get; set; }
        [Required]
        [ForeignKey("concuenta")]
        public int concuentaId { get; set; }
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
        [Required]
        public bool activo { get; set; }
        public Bancuenta bancuenta { get; set; }
        public Concuenta concuenta { get; set; }
        public Empresa empresa { get; set; }
        public IEnumerable<Lote> lotes { get; set; }
    }
}
