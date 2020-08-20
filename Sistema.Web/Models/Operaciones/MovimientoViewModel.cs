using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Operaciones
{
    public class MovimientoViewModel
    {
        public int Id { get; set; }
        public int empresaId { get; set; }
        public string empresa { get; set; }
        public int loteId { get; set; }
        public string aniomes { get; set; }
        public string asocuenta { get; set; }
        public int? asientoId { get; set; }
        public string origen { get; set; }
        public int grpconceptoId { get; set; }
        public string grpconcepto { get; set; }
        public string concepto { get; set; }
        public DateTime fecha { get; set; }
        public decimal importe { get; set; }
        public decimal unsgimporte { get; set; }
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
        public int? etlId { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
