using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechFixManager.Data;
using TechFixManager.Models;

namespace TechFixManager.Controllers
{
    public class EquiposController : Controller
    {
        private readonly AppDbContext _context;

        public EquiposController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Equipos
        public IActionResult Index()
        {
            var equipos = _context.Equipos.Include(e => e.Cliente).ToList();
            return View(equipos);
        }

        // GET: Equipos/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var equipo = _context.Equipos
                .Include(e => e.Cliente)
                .FirstOrDefault(m => m.Id == id);

            if (equipo == null) return NotFound();

            return View(equipo);
        }

        // GET: Equipos/Create
        public IActionResult Create()
        {
            ViewBag.Clientes = _context.Clientes.ToList();
            return View();
        }

        // POST: Equipos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Marca,Modelo,Serie,ClienteId")] Equipo equipo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipo);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clientes = _context.Clientes.ToList();
            return View(equipo);
        }

        // GET: Equipos/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            var equipo = _context.Equipos.Find(id);
            if (equipo == null) return NotFound();

            ViewBag.Clientes = _context.Clientes.ToList();
            return View(equipo);
        }

        // POST: Equipos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Marca,Modelo,Serie,ClienteId")] Equipo equipo)
        {
            if (id != equipo.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipo);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Equipos.Any(e => e.Id == equipo.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clientes = _context.Clientes.ToList();
            return View(equipo);
        }

        // GET: Equipos/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            var equipo = _context.Equipos
                .Include(e => e.Cliente)
                .FirstOrDefault(m => m.Id == id);

            if (equipo == null) return NotFound();

            return View(equipo);
        }

        // POST: Equipos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var equipo = _context.Equipos.Find(id);
            _context.Equipos.Remove(equipo);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}