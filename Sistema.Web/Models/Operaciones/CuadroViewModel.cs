using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Operaciones
{
    public class CuadroViewModel
    {
        public string anio { get; set; }
        public string mes { get; set; }
        public decimal contaSI { get; set; }
        public decimal contaMO { get; set; }
        public decimal contaSF { get; set; }
        public decimal partiSI { get; set; }
        public decimal partiMO { get; set; }
        public decimal partiSF { get; set; }
        public decimal bancoSI { get; set; }
        public decimal bancoMO { get; set; }
        public decimal bancoSF { get; set; }
    }
}
