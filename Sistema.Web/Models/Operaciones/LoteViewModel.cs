using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Operaciones
{
    public class LoteViewModel
    {
        public int Id { get; set; }
        public int empresaId { get; set; }
        public string empresa { get; set; }
        public int asocuentaId { get; set; }
        public string asocuenta { get; set; }
        public string anio { get; set; }
        public string mes { get; set; }
        public decimal bansalini { get; set; }
        public decimal bansalfin { get; set; }
        public decimal consalini { get; set; }
        public decimal consalfin { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
