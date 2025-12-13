using Microsoft.AspNetCore.Mvc;
using TechFixManager.Data;
using TechFixManager.Models;

namespace TechFixManager.Controllers
{
    public class RepuestosController : Controller
    {
        private readonly AppDbContext _context;

        public RepuestosController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public IActionResult Index()
        {
            var repuestos = _context.Repuestos.ToList();
            return View(repuestos);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Repuesto repuesto)
        {
            if (ModelState.IsValid)
            {
                _context.Repuestos.Add(repuesto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(repuesto);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var repuesto = _context.Repuestos.Find(id);
            if (repuesto == null)
                return NotFound();

            return View(repuesto);
        }

        // EDIT (GET)
        public IActionResult Edit(int id)
        {
            var repuesto = _context.Repuestos.Find(id);
            if (repuesto == null)
                return NotFound();

            return View(repuesto);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Repuesto repuesto)
        {
            if (id != repuesto.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(repuesto);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(repuesto);
        }

        // DELETE (GET)
        public IActionResult Delete(int id)
        {
            var repuesto = _context.Repuestos.Find(id);
            if (repuesto == null)
                return NotFound();

            return View(repuesto);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var repuesto = _context.Repuestos.Find(id);
            if (repuesto == null)
                return NotFound();

            _context.Repuestos.Remove(repuesto);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
