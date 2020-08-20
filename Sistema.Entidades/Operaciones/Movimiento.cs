using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text;

namespace Sistema.Entidades.Operaciones
{
    public class Movimiento
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [ForeignKey("empresas")]
        public int empresaId { get; set; }
        [Required]
        [ForeignKey("lotes")]
        public int loteId { get; set; }
        [ForeignKey("asiento")]
        public int? asientoId { get; set; }
        [Required]
        public string origen { get; set; }
        [Required]
        [ForeignKey("grpconcepto")]
        public int grpconceptoId { get; set; }
        [Required]
        [Column(TypeName = "varchar(100)")]
        public string concepto { get; set; }
        [Column(TypeName = "date")]
        [Required]
        public DateTime fecha { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal importe { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref0 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref1 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref2 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref3 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref4 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref5 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref6 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref7 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref8 { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string ref9 { get; set; }
        public int? etlId { get; set; }
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
        public Grpconcepto grpconcepto { get; set; }
        public Lote lote { get; set; }
        public Asiento asiento { get; set; }
    }
}
