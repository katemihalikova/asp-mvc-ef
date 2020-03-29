using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP1.Data;
using ASP1.Models;

namespace ASP1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AgencyContext _context;

        public OrdersController(AgencyContext context)
        {
            _context = context;
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int timeslotId, int attendees)
        {

            var timeslot = await _context.Timeslots
                .Include(d => d.Destination)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == timeslotId);

            var order = new Order { Timeslot = timeslot, TimeslotID = timeslotId, Attendees = attendees };

            return View(order);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeslotID,Name,Surname,Email,Phone,Attendees,Note")] Order order)
        {
            var timeslot = await _context.Timeslots
                .Include(d => d.Destination)
                .Include(d => d.Orders)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == order.TimeslotID);

            try
            {
                if (ModelState.IsValid)
                {
                    if (timeslot.FreeCapacity >= order.Attendees)
                    {
                        order.CreatedAt = DateTime.Now;
                        _context.Add(order);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Create));
                }
                else
                {
                    ModelState.AddModelError("", $"Unfortunately, your selected term now has only {timeslot.FreeCapacity} places left, but you want {order.Attendees} places. Either set fewer persons or go back and choose another term.");
                }
            }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            order.Timeslot = timeslot;
            return View(order);
        }
    }
}
