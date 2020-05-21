using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.Maestros
{
    public class Conbanco
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
        public string nombre { get; set; }
        [Required]
        public int bancoId { get; set; }
        [Required]
        [ForeignKey("grpconcepto")]
        public int grpconceptoId { get; set; }
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
        public Grpconcepto grpconcepto { get; set; }
        public Empresa empresa { get; set; }
        public Banco banco { get; set; }
    }
}
