using System.ComponentModel.DataAnnotations;

namespace TechFixManager.Models
{
    public class Pago
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Fecha de pago")]
        public DateTime FechaPago { get; set; }

        [Required]
        [Display(Name = "Concepto (Artículo o Servicio)")]
        public string Concepto { get; set; }

        [Display(Name = "Cantidad")]
        public string Cantidad { get; set; }

        [Required]
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }
    }
}