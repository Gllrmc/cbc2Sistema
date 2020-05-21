using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Personas
{
    public class PersonaViewModel
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public string domicilio { get; set; }
        public string localidad { get; set; }
        public string cpostal { get; set; }
        public int provinciaId { get; set; }
        public string provincia { get; set; }
        public int paisId { get; set; }
        public string pais { get; set; }
        public string emailpersonal { get; set; }
        public string telefonopersonal { get; set; }
        public string tipodocumento { get; set; }
        public string numdocumento { get; set; }
        public bool esempleado { get; set; }
        public bool esproveedor { get; set; }
        public bool escliente { get; set; }
        public int iduseralta { get; set; }
        public DateTime fecalta { get; set; }
        public int iduserumod { get; set; }
        public DateTime fecumod { get; set; }
        public bool activo { get; set; }
    }
}
