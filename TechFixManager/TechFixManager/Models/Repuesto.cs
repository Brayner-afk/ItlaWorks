using System.ComponentModel.DataAnnotations;

namespace TechFixManager.Models
{
    public class Repuesto
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nombre del repuesto")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Cantidad disponible")]
        public int Cantidad { get; set; }

        [Required]
        [Display(Name = "Precio")]
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; }
    }
}