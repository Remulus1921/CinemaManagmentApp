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
            //ViewData["CinemaHallId"] = new SelectList(_context.CinemaHall, "Id", "Id");
            ViewData["NrOfCinemaHall"] = new SelectList(_context.CinemaHall, "HallNr", "HallNr");
            //ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id");
            ViewData["MovieTitle"] = new SelectList(_context.Movie, "Title", "Title");
            return View();
        }

        // POST: Shows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ShowStarts,MovieId,MovieTitle,CinemaHallId,NrOfCinemaHall")] Show show)
        {
            var id = _context.Movie.Where(d => d.Title == show.MovieTitle).FirstOrDefault().Id;
            show.MovieId = id;
            var nr = _context.CinemaHall.Where(d => d.HallNr == show.NrOfCinemaHall).FirstOrDefault().Id;
            show.CinemaHallId = nr;

            if (show.ShowStarts < DateTime.Now)
            {
                return RedirectToAction(nameof(Index));
            }

            var hall = _context.CinemaHall.Where(d => d.Id == show.CinemaHallId).FirstOrDefault().AnyShows;
            if (hall)
            {
                foreach(var item in _context.Show.Where(d => d.CinemaHallId == show.CinemaHallId))
                {
                    bool showOK = show.ShowStarts == item.ShowStarts;

                    var mId = item.MovieId;

                    bool nextShow = show.ShowStarts <= item.ShowStarts
                        .AddMinutes(_context.Movie.Where(m => m.Id == mId).FirstOrDefault().MovieLenght);

                    bool eShow = show.ShowStarts >= item.ShowStarts;

                    bool privShow = show.ShowStarts.AddMinutes(_context.Movie.
                        Where(m => m.Id == show.MovieId).
                        FirstOrDefault().MovieLenght) >= item.ShowStarts;

                    if (showOK || (nextShow && eShow) || (privShow && nextShow))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                
            }
            else
            {
                _context.CinemaHall.Where(d => d.Id == show.CinemaHallId).FirstOrDefault().AnyShows = true;
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(show);
                await _context.SaveChangesAsync();
                for (int i = 1; i <= _context.CinemaHall.Where(d => d.Id == show.CinemaHallId).FirstOrDefault().NrOfSeats; i++)
                {
                    var seat = new Seat();
                    seat.SeatNumber = i;
                    seat.IsTaken = false;
                    seat.ShowId = show.Id;

                    _context.Seat.Add(seat);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CinemaHallId"] = new SelectList(_context.CinemaHall, "Id", "Id", show.CinemaHallId);
            ViewData["NrOfCinemaHall"] = new SelectList(_context.CinemaHall, "HallNr", "HallNr", show.NrOfCinemaHall);
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
            //ViewData["CinemaHallId"] = new SelectList(_context.CinemaHall, "Id", "Id", show.CinemaHallId);
            ViewData["NrOfCinemaHall"] = new SelectList(_context.CinemaHall, "HallNr", "HallNr", show.NrOfCinemaHall);
            ViewData["MovieTitle"] = new SelectList(_context.Movie, "Title", "Title", show.MovieTitle);
            //ViewData["MovieId"] = new SelectList(_context.Movie, "Id", "Id", show.MovieId);
            return View(show);
        }

        // POST: Shows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ShowStarts,MovieId,MovieTitle,NrOfCinemaHall,CinemaHallId")] Show show)
        {
            
            if (id != show.Id)
            {
                return NotFound();
            }
            var MId = _context.Movie.Where(d => d.Title == show.MovieTitle).FirstOrDefault().Id;
            show.MovieId = MId;

            var HId = _context.CinemaHall.Where(d => d.HallNr == show.NrOfCinemaHall).FirstOrDefault().Id;
            show.CinemaHallId = HId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(show);
                    await _context.SaveChangesAsync();
                    if (_context.Show.Where(d => d.CinemaHallId == show.CinemaHallId).Count() == 0)
                    {
                        _context.CinemaHall.Where(d => d.Id == show.CinemaHallId).FirstOrDefault().AnyShows = false;
                    }
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

                await _context.SaveChangesAsync();

                if (_context.Seat.Where(d => d.ShowId == show.Id)
                    .Count() > _context.CinemaHall
                    .Where(d => d.Id == show.CinemaHallId)
                    .FirstOrDefault().NrOfSeats)
                {
                    int seats = _context.Seat.Where(d => d.ShowId == show.Id)
                    .Count() - _context.CinemaHall
                    .Where(d => d.Id == show.CinemaHallId)
                    .FirstOrDefault().NrOfSeats;

                    int nrOfTheSeat = _context.Seat.Where(d => d.ShowId == show.Id).Count();

                    for (int i = seats; i > 0; i--)
                    {
                        var seatToRemove = _context.Seat.Where(d => d.ShowId == show.Id).Where(d => d.SeatNumber == nrOfTheSeat).FirstOrDefault();
                        _context.Seat.Remove(seatToRemove);
                        nrOfTheSeat--;
                        await _context.SaveChangesAsync();
                    }
                }
                else if(_context.Seat.Where(d => d.ShowId == show.Id)
                    .Count() < _context.CinemaHall
                    .Where(d => d.Id == show.CinemaHallId)
                    .FirstOrDefault().NrOfSeats)
                {
                    

                    for (int i = _context.Seat.Where(d => d.ShowId == show.Id).Count() + 1; i <= _context.CinemaHall.Where(d => d.Id == show.CinemaHallId).FirstOrDefault().NrOfSeats; i++)
                    {
                        var seat = new Seat();
                        seat.SeatNumber = i;
                        seat.ShowId = show.Id;
                        seat.IsTaken = false;

                        _context.Add(seat);
                        await _context.SaveChangesAsync();
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["CinemaHallId"] = new SelectList(_context.CinemaHall, "Id", "Id", show.CinemaHallId);
            ViewData["NrOfCinemaHall"] = new SelectList(_context.CinemaHall, "HallNr", "HallNr", show.NrOfCinemaHall);
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
            
            if (_context.Show.Where(d => d.CinemaHallId == show.CinemaHallId).Count() == 0)
            {
                _context.CinemaHall.Where(d => d.Id == show.CinemaHallId).FirstOrDefault().AnyShows = false;
            }

            foreach (var item in _context.Seat.Where(d => d.ShowId == show.Id))
            {
                _context.Seat.Remove(item);
                await _context.SaveChangesAsync();
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Reservation(int id)
        {
            return RedirectToAction("Create", "Reservations", new { id = id });
        }

        private bool ShowExists(int id)
        {
          return (_context.Show?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
