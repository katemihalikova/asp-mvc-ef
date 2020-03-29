using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP1.Data;
using ASP1.Models;
using ASP1.ViewModels;

namespace ASP1.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly AgencyContext _context;

        public DestinationsController(AgencyContext context)
        {
            _context = context;
        }

        // GET: Destinations
        public async Task<IActionResult> Index()
        {
            var destinations = await _context.Destinations
                .Include(d => d.Timeslots)
                    .ThenInclude(t => t.Orders)
                .OrderBy(d => d.Name)
                .AsNoTracking()
                .ToListAsync();

            return View(destinations);
        }

        // GET: Destinations/ChooseTimeslot/5
        public async Task<IActionResult> ChooseTimeslot(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destination = await _context.Destinations
                .Include(d => d.Timeslots)
                    .ThenInclude(t => t.Orders)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (destination == null)
            {
                return NotFound();
            }

            var viewModel = new ChooseTimeslotViewModel { Destination = destination };

            return View(viewModel);
        }

        // GET: Destinations/TimeslotsPartial/5
        public async Task<IActionResult> TimeslotsPartial(int? id, string DateFrom, string DateTo, int Attendees)
        {
            var dateFrom = DateTime.Parse(DateFrom);
            var dateTo = DateTime.Parse(DateTo);

            if (id == null)
            {
                return NotFound();
            }

            var timeslots = await _context.Timeslots
                .Where(t => t.DestinationID == id)
                .Where(t => t.DateFrom >= dateFrom && t.DateTo <= dateTo)
                .OrderBy(t => t.DateFrom)
                .Include(t => t.Destination)
                .Include(d => d.Orders)
                .AsNoTracking()
                .ToListAsync();

            timeslots = timeslots.FindAll(t => t.FreeCapacity >= Attendees);

            return PartialView("_TimeslotsPartial", timeslots);
        }
    }
}
