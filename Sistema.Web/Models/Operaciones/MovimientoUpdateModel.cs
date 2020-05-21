using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Operaciones
{
    public class MovimientoUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int empresaId { get; set; }
        [Required]
        public int loteId { get; set; }
        public int? asientoId { get; set; }
        [Required]
        public string origen { get; set; }
        [Required]
        public int grpconceptoId { get; set; }
        [Required]
        public string concepto { get; set; }
        [Required]
        public DateTime fecha { get; set; }
        [Required]
        public decimal importe { get; set; }
        public string ref0 { get; set; }
        public string ref1 { get; set; }
        public string ref2 { get; set; }
        public string ref3 { get; set; }
        public string ref4 { get; set; }
        public string ref5 { get; set; }
        public string ref6 { get; set; }
        public string ref7 { get; set; }
        public string ref8 { get; set; }
        public string ref9 { get; set; }
        [Required]
        public int etlId { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
    }
}
