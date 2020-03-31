using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ASP1.Data;
using ASP1.Models;

namespace Tests
{
    public class TestASP1Factory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<AgencyContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<AgencyContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<TestASP1Factory<TStartup>>>();

                    // Ensure the database is created.
                    db.Database.EnsureCreated();

                    try
                    {
                        SeedDb(db);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database with test data. Error: {ex.Message}");
                    }
                }
            });
        }

        private void SeedDb(AgencyContext context)
        {
            if (context.Destinations.Any())
            {
                return;
            }

            context.Destinations.Add(new Destination() { ID = 1, Name = "Earth", Description = "Mostly Harmless.", Capacity = 7 });
            context.Destinations.Add(new Destination() { ID = 2, Name = "Magrathea", Description = "An ancient planet located in the heart of the Horsehead Nebula.", Capacity = 42 });
            context.Timeslots.Add(new Timeslot { ID = 1, DestinationID = 1, DateFrom = DateTime.Parse("2020-06-01"), DateTo = DateTime.Parse("2020-06-08") });

            context.SaveChanges();
        }
    }
}
