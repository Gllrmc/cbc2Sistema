using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Bancos
{
    public class BancoViewModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string nombrecorto { get; set; }
        public string codigoBCRA { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
