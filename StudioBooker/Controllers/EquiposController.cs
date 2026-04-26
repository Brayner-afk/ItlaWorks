using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioBooker.Data;
using StudioBooker.Models;

namespace StudioBooker.Controllers;

public class EquiposController : Controller
{
    private readonly ApplicationDbContext _context;

    public EquiposController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Equipos.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var equipo = await _context.Equipos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (equipo == null) return NotFound();

        return View(equipo);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Categoria,Marca,Modelo,Descripcion,Estado")] Equipo equipo)
    {
        if (ModelState.IsValid)
        {
            _context.Add(equipo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(equipo);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var equipo = await _context.Equipos.FindAsync(id);
        if (equipo == null) return NotFound();
        return View(equipo);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Categoria,Marca,Modelo,Descripcion,Estado")] Equipo equipo)
    {
        if (id != equipo.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(equipo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Equipos.Any(e => e.Id == equipo.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(equipo);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var equipo = await _context.Equipos
            .FirstOrDefaultAsync(m => m.Id == id);
        if (equipo == null) return NotFound();

        return View(equipo);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var equipo = await _context.Equipos.FindAsync(id);
        if (equipo != null)
        {
            _context.Equipos.Remove(equipo);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
