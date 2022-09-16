using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaManagment.Areas.Identity.Data;
using CinemaManagment.Models;
using Microsoft.AspNetCore.Authorization;

namespace CinemaManagment.Controllers
{
    [Authorize]
    public class CinemaHallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemaHallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CinemaHalls
        public async Task<IActionResult> Index()
        {
              return _context.CinemaHall != null ? 
                          View(await _context.CinemaHall.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CinemaHall'  is null.");
        }

        // GET: CinemaHalls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CinemaHall == null)
            {
                return NotFound();
            }

            var cinemaHall = await _context.CinemaHall
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemaHall == null)
            {
                return NotFound();
            }

            return View(cinemaHall);
        }

        // GET: CinemaHalls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CinemaHalls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HallNr,NrOfSeats")] CinemaHall cinemaHall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinemaHall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cinemaHall);
        }

        // GET: CinemaHalls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CinemaHall == null)
            {
                return NotFound();
            }

            var cinemaHall = await _context.CinemaHall.FindAsync(id);
            if (cinemaHall == null)
            {
                return NotFound();
            }
            return View(cinemaHall);
        }

        // POST: CinemaHalls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HallNr,NrOfSeats")] CinemaHall cinemaHall)
        {
            if (id != cinemaHall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinemaHall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaHallExists(cinemaHall.Id))
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
            return View(cinemaHall);
        }

        // GET: CinemaHalls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CinemaHall == null)
            {
                return NotFound();
            }

            var cinemaHall = await _context.CinemaHall
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemaHall == null)
            {
                return NotFound();
            }

            return View(cinemaHall);
        }

        // POST: CinemaHalls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CinemaHall == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CinemaHall'  is null.");
            }
            var cinemaHall = await _context.CinemaHall.FindAsync(id);
            if (cinemaHall != null)
            {
                _context.CinemaHall.Remove(cinemaHall);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemaHallExists(int id)
        {
          return (_context.CinemaHall?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
