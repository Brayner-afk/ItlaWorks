using System.ComponentModel.DataAnnotations;

namespace TechFixManager.Models
{
    public class Reparacion
    {
        public int Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public decimal Costo { get; set; }

        [Required]
        public int EquipoId { get; set; }
        public Equipo? Equipo { get; set; }

        [Required]
        public int TecnicoId { get; set; }
        public Tecnico? Tecnico { get; set; }
    }
}
