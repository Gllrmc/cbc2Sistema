using Sistema.Entidades.Maestros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Bancuentas
{
    public class BancuentaSelectModel
    {
        public int Id { get; set; }
        public int empresaId { get; set; }
        public string empresa { get; set; }
        public string banco { get; set; }
        public string tipo { get; set; }
        public string moneda { get; set; }
        public string numcuenta { get; set; }
    }
}
