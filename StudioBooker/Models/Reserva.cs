using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace StudioBooker.Models;

public class Reserva
{
    [Key]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El cliente es obligatorio")]
    public int ClienteId { get; set; }
    
    [ValidateNever]
    public Cliente? Cliente { get; set; }

    [Required(ErrorMessage = "La cabina es obligatoria")]
    public int CabinaId { get; set; }
    
    [ValidateNever]
    public Cabina? Cabina { get; set; }

    [Required(ErrorMessage = "La fecha de inicio es obligatoria")]
    public DateTime FechaInicio { get; set; }
    
    [Required(ErrorMessage = "La fecha de fin es obligatoria")]
    public DateTime FechaFin { get; set; }

    public string Estado { get; set; } = "Pendiente";
    public string? Motivo { get; set; }
    public decimal PrecioTotal { get; set; } = 0;

    [ValidateNever]
    public ICollection<Equipo> Equipos { get; set; } = new List<Equipo>();
    
    [ValidateNever]
    public ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}
