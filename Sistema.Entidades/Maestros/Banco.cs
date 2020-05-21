using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Maestros
{
    public class Banco
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string nombrecorto { get; set; }
        public string codigoBCRA { get; set; }
        [Required]
        public int iduseralta { get; set; }
        [Required]
        public DateTime fecalta { get; set; }
        [Required]
        public int iduserumod { get; set; }
        [Required]
        public DateTime fecumod { get; set; }
        [Required]
        public bool activo { get; set; }
        public IEnumerable<Conbanco> conbancos { get; set; }
        public IEnumerable<Bancuenta> bancuentas { get; set; }
    }
}
