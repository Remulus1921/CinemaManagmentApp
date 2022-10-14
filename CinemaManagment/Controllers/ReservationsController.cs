using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaManagment.Areas.Identity.Data;
using CinemaManagment.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace CinemaManagment.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private int SN;
        private readonly ApplicationDbContext _context;
        UserManager<ApplicationUser> _userManager;
        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Reservation.Include(r => r.Show);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Show)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // GET: Reservations/Create
        public IActionResult Create(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            ViewData["SeatNr"] = new SelectList(_context.Seat.Where(d => d.ShowId == id && d.IsTaken == false)  , "SeatNumber", "SeatNumber");
            ViewData["ShowId"] = new SelectList(_context.Show.Where(d => d.Id == id)  , "Id", "Id");
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CreatorId,ShowId,SeatNr")] Reservation reservation)
        {
            SN = reservation.SeatNr;
            reservation.Id = 0;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new ArgumentException("Blad");
            }
            reservation.CreatorId = userId;
            reservation.CreatorFirstName = _context.Users.Where(d => d.Id == userId).FirstOrDefault().FirstName;
            reservation.CreatorLastName = _context.Users.Where(d => d.Id == userId).FirstOrDefault().LastName;
           
            _context.Seat.Where(d => d.ShowId == reservation.ShowId && d.SeatNumber == reservation.SeatNr).FirstOrDefault().IsTaken = true;
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        
        }

        // GET: Reservations/Edit/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }
            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            ViewData["SeatNr"] = new SelectList(_context.Seat.Where(d => d.ShowId == reservation.ShowId && d.IsTaken == false), "SeatNumber", "SeatNumber", reservation.SeatNr);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CreatorId,ShowId,SeatNr")] Reservation reservation)
        {
            var reser = await _context.Reservation.AsNoTracking().SingleOrDefaultAsync(d => d.Id == id);
            if (id != reservation.Id)
            {
                return NotFound();
            }
            SN = reser.SeatNr;
            reservation.CreatorId = reser.CreatorId;
            reservation.CreatorFirstName = reser.CreatorFirstName;
            reservation.CreatorLastName = reser.CreatorLastName;
            reservation.ShowId = reser.ShowId;
            try
            {
                _context.Update(reservation);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(reservation.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            if (reservation.SeatNr != SN)
            {
                _context.Seat.Where(d => d.SeatNumber == SN && d.ShowId == reservation.ShowId).FirstOrDefault().IsTaken = false;
                _context.Seat.Where(d => d.SeatNumber == reservation.SeatNr && d.ShowId == reservation.ShowId).FirstOrDefault().IsTaken = true;
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservation == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .Include(r => r.Show)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservation == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservation'  is null.");
            }
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Seat.Where(d => d.ShowId == reservation.ShowId && d.SeatNumber == reservation.SeatNr).FirstOrDefault().IsTaken = false;
            if (reservation != null)
            {
                _context.Reservation.Remove(reservation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Shows(int id)
        {
            return RedirectToAction("Index", "Shows");
        }
        private bool ReservationExists(int id)
        {
          return (_context.Reservation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
