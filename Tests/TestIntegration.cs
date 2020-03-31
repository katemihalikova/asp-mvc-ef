using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASP1;

namespace Tests
{
    [TestClass]
    public class TestIntegration
    {
        private HttpClient _client;

        [TestInitialize]
        public void Initialize()
        {
            TestASP1Factory<Startup> factory = new TestASP1Factory<Startup>();
            _client = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
        }

        [TestMethod]
        public void TestIndex()
        {
            var response = _client.GetAsync("/").Result;
            response.EnsureSuccessStatusCode();

            // Zde bychom oveřili správnost výsledku. Pro jednoduchost
            // zde ověříme, že se v HTML výsledného View vyskytuje název knihy
            string responseString = response.Content.ReadAsStringAsync().Result;

            Assert.IsTrue(responseString.Contains("Earth"));
            Assert.IsTrue(responseString.Contains("href=\"/Destinations/ChooseTimeslot/1\""));
            Assert.IsTrue(responseString.Contains("Magrathea"));
            Assert.IsFalse(responseString.Contains("href=\"/Destinations/ChooseTimeslot/2\""));
        }
    }
}
