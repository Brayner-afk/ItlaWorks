using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TechFixManager.Data;
using TechFixManager.Models;

namespace TechFixManager.Controllers
{
    public class ReparacionesController : Controller
    {
        private readonly AppDbContext _context;

        public ReparacionesController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public IActionResult Index()
        {
            var reparaciones = _context.Reparaciones
                .Include(r => r.Equipo)
                    .ThenInclude(e => e.Cliente)
                .Include(r => r.Tecnico)
                .ToList();

            return View(reparaciones);
        }

        // CREATE GET
        public IActionResult Create()
        {
            ViewBag.EquipoId = new SelectList(
                _context.Equipos.Include(e => e.Cliente)
                .Select(e => new
                {
                    e.Id,
                    Nombre = e.Marca + " " + e.Modelo + " (" + e.Cliente.Nombre + ")"
                }),
                "Id",
                "Nombre"
            );

            ViewBag.TecnicoId = new SelectList(_context.Tecnicos, "Id", "Nombre");

            return View();
        }

        // CREATE POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reparacion reparacion)
        {
            if (ModelState.IsValid)
            {
                _context.Reparaciones.Add(reparacion);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EquipoId = new SelectList(_context.Equipos, "Id", "Modelo", reparacion.EquipoId);
            ViewBag.TecnicoId = new SelectList(_context.Tecnicos, "Id", "Nombre", reparacion.TecnicoId);

            return View(reparacion);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var reparacion = _context.Reparaciones
                .Include(r => r.Equipo).ThenInclude(e => e.Cliente)
                .Include(r => r.Tecnico)
                .FirstOrDefault(r => r.Id == id);

            if (reparacion == null)
                return NotFound();

            return View(reparacion);
        }

        // EDIT GET
        public IActionResult Edit(int id)
        {
            var reparacion = _context.Reparaciones.Find(id);
            if (reparacion == null)
                return NotFound();

            ViewBag.EquipoId = new SelectList(_context.Equipos, "Id", "Modelo", reparacion.EquipoId);
            ViewBag.TecnicoId = new SelectList(_context.Tecnicos, "Id", "Nombre", reparacion.TecnicoId);

            return View(reparacion);
        }

        // EDIT POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reparacion reparacion)
        {
            if (id != reparacion.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(reparacion);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(reparacion);
        }

        // DELETE GET
        public IActionResult Delete(int id)
        {
            var reparacion = _context.Reparaciones
                .Include(r => r.Equipo)
                .Include(r => r.Tecnico)
                .FirstOrDefault(r => r.Id == id);

            if (reparacion == null)
                return NotFound();

            return View(reparacion);
        }

        // DELETE POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reparacion = _context.Reparaciones.Find(id);
            _context.Reparaciones.Remove(reparacion);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}