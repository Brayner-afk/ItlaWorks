using System.ComponentModel.DataAnnotations;

namespace StudioBooker.Models;

public class Cabina
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; } = string.Empty;
    public int Capacidad { get; set; }
    public string Tipo { get; set; } = string.Empty; // Ej: Audio, Video, Híbrida
    public string Estado { get; set; } = "Disponible"; // Disponible, Mantenimiento
    public string? Descripcion { get; set; }
    public string? Cualidades { get; set; }
    
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
