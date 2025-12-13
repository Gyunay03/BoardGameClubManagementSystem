using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BoardGamesClubManagementSystem.Models;

namespace BoardGamesClubManagementSystem.Controllers
{
    public class VisitorsController : Controller
    {
        private readonly BoardGamesContext _context;

        public VisitorsController(BoardGamesContext context)
        {
            _context = context;
        }

        // GET: Visitors
        public async Task<IActionResult> Index()
        {
            var boardGamesContext = _context.Posetitelis.Include(p => p.Reservation);
            return View(await boardGamesContext.ToListAsync());
        }

        // GET: Visitors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posetiteli = await _context.Posetitelis
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posetiteli == null)
            {
                return NotFound();
            }

            return View(posetiteli);
        }

        // GET: Visitors/Create
        public IActionResult Create()
        {
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationNumber", "ReservationNumber");
            return View();
        }

        // POST: Visitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,DateAndTime,ReservationId")] Posetiteli posetiteli)
        {
            if (ModelState.IsValid)
            {
                _context.Add(posetiteli);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationNumber", "ReservationNumber", posetiteli.ReservationId);
            return View(posetiteli);
        }

        // GET: Visitors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posetiteli = await _context.Posetitelis.FindAsync(id);
            if (posetiteli == null)
            {
                return NotFound();
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationNumber", "ReservationNumber", posetiteli.ReservationId);
            return View(posetiteli);
        }

        // POST: Visitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,DateAndTime,ReservationId")] Posetiteli posetiteli)
        {
            if (id != posetiteli.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(posetiteli);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PosetiteliExists(posetiteli.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReservationId"] = new SelectList(_context.Reservations, "ReservationNumber", "ReservationNumber", posetiteli.ReservationId);
            return View(posetiteli);
        }

        // GET: Visitors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posetiteli = await _context.Posetitelis
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (posetiteli == null)
            {
                return NotFound();
            }

            return View(posetiteli);
        }

        // POST: Visitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var posetiteli = await _context.Posetitelis.FindAsync(id);
            if (posetiteli != null)
            {
                _context.Posetitelis.Remove(posetiteli);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PosetiteliExists(int id)
        {
            return _context.Posetitelis.Any(e => e.Id == id);
        }
    }
}
