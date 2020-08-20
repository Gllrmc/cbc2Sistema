using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Operaciones
{
    public class AsientoSelectModel
    {
        public int Id { get; set; }
        public string comentario { get; set; }
        public int empresaId { get; set; }
        public string empresa { get; set; }
    }
}
