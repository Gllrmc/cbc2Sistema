using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Asocuentas
{
    public class AsocuentaViewModel
    {
        public int Id { get; set; }
        public int empresaId { get; set; }
        public string empresa { get; set; }
        public string orden { get; set; }
        public string descripcion { get; set; }
        public int bancuentaId { get; set; }
        public string bancuenta { get; set; }
        public int concuentaId { get; set; }
        public string concuenta { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
