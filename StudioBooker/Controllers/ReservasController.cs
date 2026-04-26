using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudioBooker.Data;
using StudioBooker.Models;

namespace StudioBooker.Controllers;

public class ReservasController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReservasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var reservas = await _context.Reservas
            .Include(r => r.Cliente)
            .Include(r => r.Cabina)
            .ToListAsync();
        return View(reservas);
    }

    public IActionResult Create()
    {
        ViewBag.CabinaId = new SelectList(_context.Cabinas.Select(c => new { 
            Id = c.Id, 
            Nombre = $"{c.Nombre} ({c.Tipo})" 
        }), "Id", "Nombre");
        ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "Nombre");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ClienteId,CabinaId,FechaInicio,FechaFin,Motivo")] Reserva reserva)
    {
        if (ModelState.IsValid)
        {
            if (!ValidarHorario(reserva))
            {
                SetCreateViewBags(reserva);
                return View(reserva);
            }

            bool solapamiento = await _context.Reservas.AnyAsync(r => 
                r.CabinaId == reserva.CabinaId && 
                r.Estado != "Cancelada" &&
                r.FechaInicio < reserva.FechaFin && 
                r.FechaFin > reserva.FechaInicio);

            if (solapamiento)
            {
                ModelState.AddModelError("", "La cabina seleccionada no está disponible en el horario especificado.");
                SetCreateViewBags(reserva);
                return View(reserva);
            }

            reserva.PrecioTotal = CalcularPrecio(reserva.FechaInicio, reserva.FechaFin);
            _context.Add(reserva);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        SetCreateViewBags(reserva);
        return View(reserva);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva == null) return NotFound();

        SetCreateViewBags(reserva);
        return View(reserva);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,CabinaId,FechaInicio,FechaFin,Estado,Motivo")] Reserva reserva)
    {
        if (id != reserva.Id) return NotFound();

        if (ModelState.IsValid)
        {
            if (!ValidarHorario(reserva))
            {
                SetCreateViewBags(reserva);
                return View(reserva);
            }

            try
            {
                reserva.PrecioTotal = CalcularPrecio(reserva.FechaInicio, reserva.FechaFin);
                _context.Update(reserva);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Reservas.Any(e => e.Id == reserva.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        SetCreateViewBags(reserva);
        return View(reserva);
    }

    private bool ValidarHorario(Reserva reserva)
    {
        if (reserva.FechaFin <= reserva.FechaInicio)
        {
            ModelState.AddModelError("", "La fecha de fin debe ser posterior a la fecha de inicio.");
            return false;
        }

        if (reserva.FechaInicio.Hour < 15 || reserva.FechaFin.Hour > 23 || (reserva.FechaFin.Hour == 23 && reserva.FechaFin.Minute > 0))
        {
            ModelState.AddModelError("", "El horario de reserva debe estar entre las 15:00 y las 23:00.");
            return false;
        }

        return true;
    }

    private decimal CalcularPrecio(DateTime inicio, DateTime fin)
    {
        var duracion = fin - inicio;
        decimal horas = (decimal)duracion.TotalHours;
        
        // Sesiones menores a 1 hora: $50 fijo.
        if (horas < 1)
        {
            return 50;
        }
        
        // Sesiones de 1 hora en adelante: $80 por hora.
        return horas * 80;
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var reserva = await _context.Reservas
            .Include(r => r.Cabina)
            .Include(r => r.Cliente)
            .FirstOrDefaultAsync(m => m.Id == id);
            
        if (reserva == null) return NotFound();

        return View(reserva);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva != null) _context.Reservas.Remove(reserva);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Confirmar(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva != null)
        {
            reserva.Estado = "Confirmada";
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Cancelar(int id)
    {
        var reserva = await _context.Reservas.FindAsync(id);
        if (reserva != null)
        {
            reserva.Estado = "Cancelada";
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private void SetCreateViewBags(Reserva reserva)
    {
        ViewBag.CabinaId = new SelectList(_context.Cabinas.Select(c => new { 
            Id = c.Id, 
            Nombre = $"{c.Nombre} ({c.Tipo})" 
        }), "Id", "Nombre", reserva.CabinaId);
        ViewBag.ClienteId = new SelectList(_context.Clientes, "Id", "Nombre", reserva.ClienteId);
    }
}
