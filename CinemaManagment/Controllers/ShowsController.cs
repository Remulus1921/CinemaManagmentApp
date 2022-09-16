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
    public class ShowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Shows
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Show.Include(s => s.Hall).Include(s => s.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Shows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // GET: Shows/Create
        public IActionResult Create()
        {
            ViewData["CinemaHallId"] = new SelectList(_context.CinemaHall, "Id", "Id");
            //ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id");
            ViewData["MovieTitle"] = new SelectList(_context.Movie, "Title", "Title");
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShowStarts,MovieId,MovieTitle,CinemaHallId")] Show show)
        {
            var id = _context.Movie.Where(d => d.Title == show.MovieTitle).FirstOrDefault().Id;
            show.MovieId = id;
            if (ModelState.IsValid)
            {
                _context.Add(show);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaHallId"] = new SelectList(_context.CinemaHall, "Id", "Id", show.CinemaHallId);
            //ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", show.MovieId);
            ViewData["MovieTitle"] = new SelectList(_context.Movie, "Title", "Title", show.MovieTitle);
            return View(show);
        }

        // GET: Shows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }
            ViewData["CinemaHallId"] = new SelectList(_context.CinemaHall, "Id", "Id", show.CinemaHallId);
            ViewData["MovieTitle"] = new SelectList(_context.Movie, "Title", "Title", show.MovieTitle);
            ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", show.MovieId);
            return View(show);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShowStarts,MovieId,MovieTitle,CinemaHallId")] Show show)
        {
            var MId = _context.Movie.Where(d => d.Title == show.MovieTitle).FirstOrDefault().Id;
            show.MovieId = MId;
            if (id != show.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShowExists(show.Id))
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
            ViewData["CinemaHallId"] = new SelectList(_context.CinemaHall, "Id", "Id", show.CinemaHallId);
            ViewData["MovieTitle"] = new SelectList(_context.Movie, "Title", "Title", show.MovieTitle);
            //ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", show.MovieId);
            return View(show);
        }

        // GET: Shows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Show == null)
            {
                return NotFound();
            }

            var show = await _context.Show
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (show == null)
            {
                return NotFound();
            }

            return View(show);
        }

        // POST: Shows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Show == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Show'  is null.");
            }
            var show = await _context.Show.FindAsync(id);
            if (show != null)
            {
                _context.Show.Remove(show);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShowExists(int id)
        {
          return (_context.Show?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
