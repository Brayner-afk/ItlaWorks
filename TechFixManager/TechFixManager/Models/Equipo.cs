using System.ComponentModel.DataAnnotations;

namespace TechFixManager.Models
{
    public class Equipo
    {
        public int Id { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public string Serie { get; set; }

        [Required]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}