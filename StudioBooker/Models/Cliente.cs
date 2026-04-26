using System.ComponentModel.DataAnnotations;

namespace StudioBooker.Models;

public class Cliente
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; } = string.Empty;
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    
    public ICollection<Reserva> HistorialReservas { get; set; } = new List<Reserva>();
}
