using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Provincias
{
    public class ProvinciaViewModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public int paisId { get; set; }
        public string pais { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
