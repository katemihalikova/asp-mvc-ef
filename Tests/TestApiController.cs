using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ASP1.Controllers;
using ASP1.Models;
using ASP1.ViewModels;
using ASP1.Dto;
using ASP1.Services;

namespace Tests
{
    [TestClass]
    public class TestApiController
    {
        [TestMethod]
        public async Task TestGetDestinations()
        {
            var destinations = new List<Destination>
            {
                new Destination { ID = 1, Name = "Earth", Description = "Mostly Harmless.", Capacity = 7, Timeslots = new List<Timeslot>() },
                new Destination { ID = 2, Name = "Magrathea", Description = "An ancient planet located in the heart of the Horsehead Nebula.", Capacity = 42, Timeslots = new List<Timeslot>() },
            };

            destinations[0].Timeslots.Add(new Timeslot { ID = 1, Destination = destinations[0], DateFrom = DateTime.Parse("2020-06-01"), DateTo = DateTime.Parse("2020-06-08"), Orders = new List<Order>() });
            destinations[0].Timeslots.Add(new Timeslot { ID = 2, Destination = destinations[0], DateFrom = DateTime.Parse("2020-06-03"), DateTo = DateTime.Parse("2020-06-11"), Orders = new List<Order>() });
            destinations[0].Timeslots.Add(new Timeslot { ID = 3, Destination = destinations[0], DateFrom = DateTime.Parse("2020-08-21"), DateTo = DateTime.Parse("2020-08-28"), Orders = new List<Order>() });

            destinations[0].Timeslots.ElementAt(0).Orders.Add(new Order { Attendees = 7 });

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetDestinations()).Returns(Task.FromResult(destinations as IEnumerable<Destination>));
            var apiController = new ApiController(repositoryMock.Object);

            IConvertToActionResult result = await apiController.GetDestinations("5.6.2020");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<DestinationDto>>));

            var unwrappedValue = (result as ActionResult<IEnumerable<DestinationDto>>).Value;
            Assert.IsInstanceOfType(unwrappedValue, typeof(IEnumerable<DestinationDto>));

            //var viewModel = viewResult.ViewData.Model as IEnumerable<Destination>;
            Assert.AreEqual(2, unwrappedValue.Count());

            Assert.AreEqual("Earth", unwrappedValue.ElementAt(0).name);
            Assert.AreEqual(1, unwrappedValue.ElementAt(0).timeslots.Count());
            
            Assert.AreEqual("Magrathea", unwrappedValue.ElementAt(1).name);
            Assert.AreEqual(0, unwrappedValue.ElementAt(1).timeslots.Count());
        }

        [TestMethod]
        public async Task TestGetDestinationsWrongDateFormat()
        {
            var repositoryMock = new Mock<IRepository>();
            var apiController = new ApiController(repositoryMock.Object);

            IConvertToActionResult result = await apiController.GetDestinations("not a date");
            Assert.IsInstanceOfType(result, typeof(ActionResult<IEnumerable<DestinationDto>>));

            var unwrappedResult = (result as ActionResult<IEnumerable<DestinationDto>>).Result;
            Assert.IsInstanceOfType(unwrappedResult, typeof(BadRequestObjectResult));
        }
    }
}
