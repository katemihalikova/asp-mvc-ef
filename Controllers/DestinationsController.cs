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
    }
}
