using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HopIn.Data;
using HopIn.Models;

namespace HopIn.Controllers
{
    public class RideRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RideRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RideRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.RideRequests.Include(r => r.Passenger).Include(r => r.Ride);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: RideRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rideRequest = await _context.RideRequests
                .Include(r => r.Passenger)
                .Include(r => r.Ride)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (rideRequest == null)
            {
                return NotFound();
            }

            return View(rideRequest);
        }

        // GET: RideRequests/Create
        public IActionResult Create()
        {
            ViewData["PassengerId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["RideId"] = new SelectList(_context.Rides, "RideId", "Destination");
            return View();
        }

        // POST: RideRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RequestId,RideId,PassengerId,Status,Message,AgreedCost")] RideRequest rideRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rideRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PassengerId"] = new SelectList(_context.Users, "Id", "Id", rideRequest.PassengerId);
            ViewData["RideId"] = new SelectList(_context.Rides, "RideId", "Destination", rideRequest.RideId);
            return View(rideRequest);
        }

        // GET: RideRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rideRequest = await _context.RideRequests.FindAsync(id);
            if (rideRequest == null)
            {
                return NotFound();
            }
            ViewData["PassengerId"] = new SelectList(_context.Users, "Id", "Id", rideRequest.PassengerId);
            ViewData["RideId"] = new SelectList(_context.Rides, "RideId", "Destination", rideRequest.RideId);
            return View(rideRequest);
        }

        // POST: RideRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RequestId,RideId,PassengerId,Status,Message,AgreedCost")] RideRequest rideRequest)
        {
            if (id != rideRequest.RequestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rideRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RideRequestExists(rideRequest.RequestId))
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
            ViewData["PassengerId"] = new SelectList(_context.Users, "Id", "Id", rideRequest.PassengerId);
            ViewData["RideId"] = new SelectList(_context.Rides, "RideId", "Destination", rideRequest.RideId);
            return View(rideRequest);
        }

        // GET: RideRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rideRequest = await _context.RideRequests
                .Include(r => r.Passenger)
                .Include(r => r.Ride)
                .FirstOrDefaultAsync(m => m.RequestId == id);
            if (rideRequest == null)
            {
                return NotFound();
            }

            return View(rideRequest);
        }

        // POST: RideRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rideRequest = await _context.RideRequests.FindAsync(id);
            if (rideRequest != null)
            {
                _context.RideRequests.Remove(rideRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RideRequestExists(int id)
        {
            return _context.RideRequests.Any(e => e.RequestId == id);
        }
    }
}
