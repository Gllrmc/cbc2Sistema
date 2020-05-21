using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models.Maestros.Appconfig
{
    public class AppconfigUpdateModel
    {
        [Required]
        public int id { get; set; }
        public string parametro { get; set; }
        public string vstring { get; set; }
        public int? vint { get; set; }
        public decimal? vdecimal { get; set; }
        public DateTime? vdatetime { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
    }
}
