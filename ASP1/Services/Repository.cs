using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ASP1.Models;
using ASP1.Data;

namespace ASP1.Services
{
    public class Repository : IRepository
    {
        private AgencyContext _context;

        public Repository(AgencyContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Destination>> GetDestinations()
        {
            return await _context.Destinations
                .Include(d => d.Timeslots)
                    .ThenInclude(t => t.Orders)
                .OrderBy(d => d.Name)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Destination> GetDestinationByID(int id)
        {
            return await _context.Destinations
                .Include(d => d.Timeslots)
                    .ThenInclude(t => t.Orders)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public async Task<IEnumerable<Timeslot>> GetTimeslots()
        {
            return await _context.Timeslots
                .Include(t => t.Destination)
                .Include(t => t.Orders)
                .OrderBy(t => t.DateFrom)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Timeslot> GetTimeslotByID(int id)
        {
            return await _context.Timeslots
                .Include(t => t.Destination)
                .Include(t => t.Orders)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
        }

        public void InsertOrder(Order order)
        {
            _context.Add(order);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
