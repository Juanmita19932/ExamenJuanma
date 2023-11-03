using ExamenJuanma.Models;
using ExamenJuanManuelMuñiz.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamenJuanma.Controllers
{
    public class ReservaController : Controller
    {
        private readonly DemoContext _context;

        public ReservaController(DemoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Reserva> listaReservas = _context.Reservas.ToList();
            return View(listaReservas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Reservas.Add(reserva);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reserva);
        }

        public IActionResult Edit(int id)
        {
            Reserva reserva = _context.Reservas.Find(id);

            if (reserva != null)
            {
                return View(reserva);
            }

            throw new Exception("Error al encontrar la reserva");
        }

        [HttpPost]
        public IActionResult Edit(int id, Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _context.Reservas.Update(reserva);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reserva);
        }

        public IActionResult Delete(int id)
        {
            Reserva producto = _context.Reservas.Find(id);

            if (producto != null)
            {
                _context.Reservas.Remove(producto);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            throw new Exception("Error al eliminar la reserva");
        }
    }
}
