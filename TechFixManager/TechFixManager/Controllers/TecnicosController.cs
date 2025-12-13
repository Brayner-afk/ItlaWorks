using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechFixManager.Data;
using TechFixManager.Models;

namespace TechFixManager.Controllers
{
    public class TecnicosController : Controller
    {
        private readonly AppDbContext _context;

        public TecnicosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tecnicos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tecnicos.ToListAsync());
        }

        // GET: Tecnicos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tecnico == null) return NotFound();

            return View(tecnico);
        }

        // GET: Tecnicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tecnicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tecnico tecnico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tecnico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tecnico);
        }

        // GET: Tecnicos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico == null) return NotFound();

            return View(tecnico);
        }

        // POST: Tecnicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Tecnico tecnico)
        {
            if (id != tecnico.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tecnico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Tecnicos.Any(e => e.Id == tecnico.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tecnico);
        }

        // GET: Tecnicos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var tecnico = await _context.Tecnicos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tecnico == null) return NotFound();

            return View(tecnico);
        }

        // POST: Tecnicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico != null)
            {
                _context.Tecnicos.Remove(tecnico);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}