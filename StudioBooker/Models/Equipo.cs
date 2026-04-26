using System.ComponentModel.DataAnnotations;

namespace StudioBooker.Models;

public class Equipo
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty; // Micrófono, Consola, Cámara
    public string? Marca { get; set; }
    public string? Modelo { get; set; }
    public string? Descripcion { get; set; }
    public string Estado { get; set; } = "Disponible"; // Disponible, En Uso, Mantenimiento
    
    public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
