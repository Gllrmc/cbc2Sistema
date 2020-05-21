using System;

namespace Sistema.Web.Models.Maestros.Paises
{
    public class PaisViewModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string cuit { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
