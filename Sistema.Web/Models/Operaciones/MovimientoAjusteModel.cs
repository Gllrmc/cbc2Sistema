using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Operaciones
{
    public class MovimientoAjusteModel
    {
        [Required]
        public int[] Id { get; set; }
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
        public int? etlId { get; set; }
        [Required]
        public int iduseralta { get; set; }
    }
}
