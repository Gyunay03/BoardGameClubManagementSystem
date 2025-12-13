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
    public class OrdersController : Controller
    {
        private readonly BoardGamesContext _context;

        public OrdersController(BoardGamesContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var boardGamesContext = _context.Orders.Include(o => o.Client).Include(o => o.Event).Include(o => o.Game).Include(o => o.Menu);
            return View(await boardGamesContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Event)
                .Include(o => o.Game)
                .Include(o => o.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Posetitelis, "Id", "Id");
            ViewData["EventId"] = new SelectList(_context.Events, "EventNumber", "EventNumber");
            ViewData["GameId"] = new SelectList(_context.BoardGames, "Id", "Id");
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,OrderNumber,ClientId,GameId,EventId,MenuId,CreatedAt")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.Posetitelis, "Id", "Id", order.ClientId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventNumber", "EventNumber", order.EventId);
            ViewData["GameId"] = new SelectList(_context.BoardGames, "Id", "Id", order.GameId);
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", order.MenuId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.Posetitelis, "Id", "Id", order.ClientId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventNumber", "EventNumber", order.EventId);
            ViewData["GameId"] = new SelectList(_context.BoardGames, "Id", "Id", order.GameId);
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", order.MenuId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,OrderNumber,ClientId,GameId,EventId,MenuId,CreatedAt")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["ClientId"] = new SelectList(_context.Posetitelis, "Id", "Id", order.ClientId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventNumber", "EventNumber", order.EventId);
            ViewData["GameId"] = new SelectList(_context.BoardGames, "Id", "Id", order.GameId);
            ViewData["MenuId"] = new SelectList(_context.Menus, "MenuId", "MenuId", order.MenuId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Client)
                .Include(o => o.Event)
                .Include(o => o.Game)
                .Include(o => o.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
