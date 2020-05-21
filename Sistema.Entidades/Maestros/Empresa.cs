using Sistema.Entidades.Operaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sistema.Entidades.Maestros
{
    public class Empresa
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string nombre { get; set; }
        [Required]
        public string cuit { get; set; }
        public string direccion { get; set; }
        public string localidad { get; set; }
        public string cpostal { get; set; }
        [Required]
        public int provinciaId { get; set; }
        [Required]
        public int paisId { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string webpage { get; set; }
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

        public Pais pais { get; set; }
        public Provincia provincia { get; set; }
        public IEnumerable<Conbanco> conbancos { get; set; }
        public IEnumerable<Conconta> concontas { get; set; }
        public IEnumerable<Bancuenta> bancuentas { get; set; }
        public IEnumerable<Concuenta> concuentas { get; set; }
        public IEnumerable<Asiento> asientos { get; set; }
        public IEnumerable<Lote> lotes { get; set; }
        public IEnumerable<Movimiento> movimientos { get; set; }
    }
}
