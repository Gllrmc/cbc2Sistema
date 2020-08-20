using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Operaciones
{
    public class AsientoViewModel
    {
        public int Id { get; set; }
        public int empresaId { get; set; }
        public string empresa { get; set; }
        public string comentario { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }

    }
}
