using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ASP1.Controllers;
using ASP1.Models;
using ASP1.ViewModels;
using ASP1.Services;

namespace Tests
{
    [TestClass]
    public class TestDestinationsController
    {
        [TestMethod]
        public async Task TestIndex()
        {
            IEnumerable<Destination> destinations = new List<Destination>
            {
                new Destination { ID = 1, Name = "Earth", Description = "Mostly Harmless.", Capacity = 7 },
                new Destination { ID = 2, Name = "Magrathea", Description = "An ancient planet located in the heart of the Horsehead Nebula.", Capacity = 42 },
            };

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetDestinations()).Returns(Task.FromResult(destinations));
            var destinationsController = new DestinationsController(repositoryMock.Object);

            IActionResult result = await destinationsController.Index();
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(IEnumerable<Destination>));

            var viewModel = viewResult.ViewData.Model as IEnumerable<Destination>;
            Assert.AreEqual(2, viewModel.Count());
        }

        [TestMethod]
        public async Task TestChooseTimeslotExisting()
        {
            var destination = new Destination { ID = 1, Name = "Earth", Description = "Mostly Harmless.", Capacity = 7 };

            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetDestinationByID(1)).Returns(Task.FromResult(destination));
            var destinationsController = new DestinationsController(repositoryMock.Object);

            IActionResult result = await destinationsController.ChooseTimeslot(1);
            Assert.IsInstanceOfType(result, typeof(ViewResult));

            var viewResult = result as ViewResult;
            Assert.IsInstanceOfType(viewResult.ViewData.Model, typeof(ChooseTimeslotViewModel));

            var viewModel = viewResult.ViewData.Model as ChooseTimeslotViewModel;
            Assert.AreEqual(destination, viewModel.Destination);
        }

        [TestMethod]
        public async Task TestChooseTimeslotNotExisting()
        {
            var repositoryMock = new Mock<IRepository>();
            repositoryMock.Setup(r => r.GetDestinationByID(1)).Returns(Task.FromResult(null as Destination));
            var destinationsController = new DestinationsController(repositoryMock.Object);

            IActionResult result = await destinationsController.ChooseTimeslot(1);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }
    }
}
