using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sistema.Entidades.Maestros
{
    public class Appconfig
    {
        [Key]
        public int id { get; set; }
        public string parametro { get; set; }
        public string vstring { get; set; }
        public int? vint { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? vdecimal { get; set; }
        public DateTime? vdatetime { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
