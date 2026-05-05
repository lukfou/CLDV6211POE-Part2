using CLDV6211POE.Data;
using CLDV6211POE.Models;
using CLDV6211POE.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CLDV6211POE.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // =========================
        // INDEX (ENHANCED DISPLAY + SEARCH)
        // =========================
        public async Task<IActionResult> Index(string searchString)
        {
            var bookings = from b in _context.Bookings
                           join v in _context.Venues on b.VenueId equals v.VenueId
                           join e in _context.Events on b.EventId equals e.EventId
                           select new BookingViewModel
                           {
                               BookingId = b.BookingId,
                               VenueName = v.Name,
                               EventName = e.Name,
                               Location = v.Location,
                               Capacity = v.Capacity,
                               EventStart = e.StartDate,
                               EventEnd = e.EndDate
                           };

            // 🔍 SEARCH FUNCTION
            if (!string.IsNullOrEmpty(searchString))
            {
                bookings = bookings.Where(b =>
                    b.EventName.Contains(searchString) ||
                    b.BookingId.ToString().Contains(searchString));
            }

            return View(await bookings.ToListAsync());
        }

        // =========================
        // DETAILS
        // =========================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // =========================
        // CREATE (GET)
        // =========================
        public IActionResult Create()
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name");
            return View();
        }

        // =========================
        // CREATE (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            try
            {
                // 🚫 Prevent duplicate booking (same venue + event)
                var exists = _context.Bookings.Any(b =>
                    b.VenueId == booking.VenueId &&
                    b.EventId == booking.EventId);

                if (exists)
                {
                    ModelState.AddModelError("", "This venue is already booked for this event.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating booking: " + ex.Message);
            }

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", booking.EventId);

            return View(booking);
        }

        // =========================
        // EDIT (GET)
        // =========================
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", booking.EventId);

            return View(booking);
        }

        // =========================
        // EDIT (POST)
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.BookingId) return NotFound();

            try
            {
                if (ModelState.IsValid)
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(booking.BookingId))
                    return NotFound();
                else
                    throw;
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error updating booking: " + ex.Message);
            }

            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", booking.EventId);

            return View(booking);
        }

        // =========================
        // DELETE (GET)
        // =========================
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings
                .Include(b => b.Venue)
                .Include(b => b.Event)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null) return NotFound();

            return View(booking);
        }

        // =========================
        // DELETE (POST)
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);

            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // HELPER METHOD
        // =========================
        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
