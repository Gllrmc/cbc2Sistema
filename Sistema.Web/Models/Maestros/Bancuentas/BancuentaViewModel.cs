using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Bancuentas
{
    public class BancuentaViewModel
    {
        public int Id { get; set; }
        public int empresaId { get; set; }
        public string empresa { get; set; }
        public int bancoId { get; set; }
        public string banco { get; set; }
        public string numcuenta { get; set; }
        public string tipo { get; set; }
        public string moneda { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
