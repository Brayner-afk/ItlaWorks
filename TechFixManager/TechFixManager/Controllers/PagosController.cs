using Microsoft.AspNetCore.Mvc;
using TechFixManager.Data;
using TechFixManager.Models;

namespace TechFixManager.Controllers
{
    public class PagosController : Controller
    {
        private readonly AppDbContext _context;

        public PagosController(AppDbContext context)
        {
            _context = context;
        }

        // INDEX
        public IActionResult Index()
        {
            return View(_context.Pagos.ToList());
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            var pago = new Pago
            {
                FechaPago = DateTime.Now
            };
            return View(pago);
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pago pago)
        {
            if (ModelState.IsValid)
            {
                _context.Pagos.Add(pago);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(pago);
        }

        // DETAILS
        public IActionResult Details(int id)
        {
            var pago = _context.Pagos.Find(id);
            if (pago == null)
                return NotFound();

            return View(pago);
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            var pago = _context.Pagos.Find(id);
            if (pago == null)
                return NotFound();

            return View(pago);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var pago = _context.Pagos.Find(id);
            _context.Pagos.Remove(pago);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}