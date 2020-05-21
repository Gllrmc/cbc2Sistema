using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Concuentas
{
    public class ConcuentaSelectModel
    {
        public int Id { get; set; }
        public int empresaId { get; set; }
        public string empresa { get; set; }
        public string apporigen { get; set; }
        public string moneda { get; set; }
        public string numcuenta { get; set; }
    }
}
