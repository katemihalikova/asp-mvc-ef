using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP1.Data;
using ASP1.Models;
using ASP1.Dto;

namespace ASP1.Controllers
{
    [Route("api/destinations")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly AgencyContext _context;

        public ApiController(AgencyContext context)
        {
            _context = context;
        }

        // GET: api/destinations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DestinationDto>>> GetDestinations(string date)
        {
            DateTime parsedDate;

            try
            {
                parsedDate = DateTime.Parse(date);
            }
            catch (FormatException)
            {
                return BadRequest(new { error = "Parameter 'date' must be a valid date." });
            }
            catch (ArgumentNullException)
            {
                return BadRequest(new { error = "Parameter 'date' is required." });
            }

            return await _context.Destinations
                .Include(d => d.Timeslots)
                    .ThenInclude(t => t.Orders)
                .OrderBy(d => d.Name)
                .Select(d => new {
                    destination = d,
                    timeslots = d.Timeslots
                    .Where(t => t.DateFrom <= parsedDate && t.DateTo >= parsedDate && t.FreeCapacity > 0)
                    .OrderBy(t => t.DateFrom)
                })
                .Select(o => DestinationToDto(o.destination, o.timeslots))
                .ToListAsync();
        }

        // GET: api/destinations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DestinationDto>> GetDestination(int id, string date)
        {
            DateTime parsedDate;

            try
            {
                parsedDate = DateTime.Parse(date);
            }
            catch (FormatException)
            {
                return BadRequest(new { error = "Parameter 'date' must be a valid date." });
            }
            catch (ArgumentNullException)
            {
                return BadRequest(new { error = "Parameter 'date' is required." });
            }

            var destination = await _context.Destinations
                .Include(d => d.Timeslots)
                    .ThenInclude(t => t.Orders)
                .FirstOrDefaultAsync(d => d.ID == id);

            if (destination == null)
            {
                return NotFound();
            }

            var timeslots = destination.Timeslots
                .Where(t => t.DateFrom <= parsedDate && t.DateTo >= parsedDate && t.FreeCapacity > 0)
                .OrderBy(t => t.DateFrom);

            return DestinationToDto(destination, timeslots);
        }

        private static DestinationDto DestinationToDto(Destination d, IEnumerable<Timeslot> t) =>
            new DestinationDto
            {
                id = d.ID,
                name = d.Name,
                description = d.Description,
                capacity = d.Capacity,
                timeslots = t.Select(i => TimeslotToDto(i)),
            };

        private static TimeslotDto TimeslotToDto(Timeslot t) =>
            new TimeslotDto
            {
                id = t.ID,
                dateFrom = t.DateFrom.ToString("dd.MM.yyyy"),
                dateTo = t.DateTo.ToString("dd.MM.yyyy"),
                freeCapacity = t.FreeCapacity,
            };
    }
}
