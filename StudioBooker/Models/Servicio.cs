using System.ComponentModel.DataAnnotations;

namespace StudioBooker.Models;

public class Servicio
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; } = string.Empty;
    public decimal Costo { get; set; }
    
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
