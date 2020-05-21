using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Bancos
{
    public class BancoUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string nombrecorto { get; set; }
        [Required]
        public string codigoBCRA { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
    }
}
