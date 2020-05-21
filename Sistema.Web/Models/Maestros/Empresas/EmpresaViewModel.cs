
using System;

namespace Sistema.Web.Models.Maestros.Empresas
{
    public class EmpresaViewModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string cuit { get; set; }
        public string direccion { get; set; }
        public string localidad { get; set; }
        public string cpostal { get; set; }
        public int provinciaId { get; set; }
        public string provincia { get; set; }
        public int paisId { get; set; }
        public string pais { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string webpage { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
