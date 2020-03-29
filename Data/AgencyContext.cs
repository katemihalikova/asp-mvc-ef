using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP1.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP1.Data
{
    public class AgencyContext : DbContext
    {
        public AgencyContext(DbContextOptions<AgencyContext> options) : base(options)
        {
        }

        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Timeslot> Timeslots { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Timeslot>()
                .HasOne(b => b.Destination)
                .WithMany(a => a.Timeslots)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
