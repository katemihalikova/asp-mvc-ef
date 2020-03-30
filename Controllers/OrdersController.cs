using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP1.Services;
using ASP1.Models;

namespace ASP1.Controllers
{
    public class OrdersController : Controller
    {
        private IRepository _repository;

        public OrdersController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Orders/Create
        public async Task<IActionResult> Create(int timeslotId, int attendees)
        {

            var timeslot = await _repository.GetTimeslotByID(timeslotId);

            var order = new Order { Timeslot = timeslot, TimeslotID = timeslotId, Attendees = attendees };

            return View(order);
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeslotID,Name,Surname,Email,Phone,Attendees,Note")] Order order)
        {
            var timeslot = await _repository.GetTimeslotByID(order.TimeslotID);

            if (timeslot == null)
            {
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    if (timeslot.FreeCapacity >= order.Attendees)
                    {
                        order.CreatedAt = DateTime.Now;
                        _repository.InsertOrder(order);
                        await _repository.Save();
                        return RedirectToAction("Index", "Destinations");
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
