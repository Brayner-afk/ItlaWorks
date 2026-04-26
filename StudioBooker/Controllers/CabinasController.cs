using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudioBooker.Data;
using StudioBooker.Models;

namespace StudioBooker.Controllers;

public class CabinasController : Controller
{
    private readonly ApplicationDbContext _context;

    public CabinasController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Cabinas.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var cabina = await _context.Cabinas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cabina == null) return NotFound();

        return View(cabina);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Capacidad,Tipo,Estado,Descripcion,Cualidades")] Cabina cabina)
    {
        if (ModelState.IsValid)
        {
            _context.Add(cabina);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(cabina);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var cabina = await _context.Cabinas.FindAsync(id);
        if (cabina == null) return NotFound();
        return View(cabina);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Capacidad,Tipo,Estado,Descripcion,Cualidades")] Cabina cabina)
    {
        if (id != cabina.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(cabina);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cabinas.Any(e => e.Id == cabina.Id)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(cabina);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var cabina = await _context.Cabinas
            .FirstOrDefaultAsync(m => m.Id == id);
        if (cabina == null) return NotFound();

        return View(cabina);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var cabina = await _context.Cabinas.FindAsync(id);
        if (cabina != null)
        {
            _context.Cabinas.Remove(cabina);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
