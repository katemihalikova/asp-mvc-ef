using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP1.Models;

namespace ASP1.Data
{
    public class DbInitializer
    {
        public static void Initialize(AgencyContext context)
        {
            context.Database.EnsureCreated();

            // Pokud už je v databázi nějaká destinace, databáze je už naplněná
            if (context.Destinations.Any())
            {
                return;
            }

            var rand = new Random();

            // dovolenkové destinace
            var destinations = new Destination[]
            {
                new Destination { Name = "Allosimanius Syneca", Description = "A cold, snowy, beautiful planet. It is so beautiful that if you stood on top of the Ice Crystal Pyramids of Sastantua, it is possible that your brain will fall out due to unobserved beauty.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Betelgeuse V", Description = "The fifth planet of the star system XY8S Z GAMMA, the closest red supergiant which holds two, maybe three, planets. Betelgeuse's girls \"will knock you off your feet\", according to a chant recited by crowds outside the Sirius Cybernetics Corporation Teleport Systems factory on Happi-Werld III.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Bethselamin", Description = "For years, the fabulously beautiful planet of Bethselamin increased its booming tourist industry without any worries at all. Alas, as is often the case, this was an act of utter stupidity, as it led to a colossal cumulative erosion problem. Of course, what else could one expect with ten billion tourists per annum?", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Broop Kidron Thirteen", Description = "The home planet of the Shaltanac race. It is also where the joopleberry shrub can be found, and is a planet known for being \"somewhat eccentric botanically speaking.\"", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Fallia", Description = "A planet famous for its marshes, which emit a deadly hallucinogenic euphoria, which has killed many hikers happily.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Han Wavel", Description = "One of the Universe's Wonders, a world of fantastic ultra-luxury, created by natural erosion of rock. Many scientists came here to try and figure out how this Infinity-1 to one chance came about.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Jaglan Beta", Description = "The second planet from the star Jaglan, near the Axel Nebula. It has at least three, cold moons. It is featured in the popular tune, \"I Left My Leg in Jaglan Beta\".", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Joltrast 3", Description = "A planet in Sector HKF 58 P on which Ziggie's Den of Iniquity resides, which is seen as one of the best places to get a Pan Galactic Gargle Blaster. One can expect to pay 104 Altairian dollars for a Pan Galactic Gargle Blaster on this planet.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Kakrafoon Kappa", Description = "The tenth planet of the Kakrafoon system. A planet comprised almost wholly of arid red desert, it is home to an annoyingly accomplished and enlightened race called the Belcerebons.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Kria", Description = "The planet home to the Asgoths, who make the second worst poetry in the Universe.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Lazgar Beta", Description = "A planet in Sector RT 74 on which the Bistro Illegal resides, which is seen as one of the best places to get a Pan Galactic Gargle Blaster. One can expect to pay around 90 Altairian dollars for a Pan Galactic Gargle Blaster on this planet.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Magrathea", Description = "An ancient planet located in orbit around the twin suns Soulianis and Rahm in the heart of the Horsehead Nebula.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Nephologia", Description = "A planet famous for its misty plains which are permanently fogbound.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Oglaroon", Description = "A large forest planet. The entire intelligent population of the planet lives in one small nut tree.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Preliumtarn", Description = "A planet that orbits the star Zarss in Galactic Sector QQ7 Active J Gamma. The planet is famous for its two mountain chains, Quentulus Quazgar and Sevorbeupstry.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Santraginus V", Description = "The fifth planet from the Santraginus star. It is home to marble-sanded beaches containing \"precious seashells\", with seas full of \"beautiful\" sea-fish and sea-water, the last of which is part of the alcoholic drink the Pan Galactic Gargle Blaster.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Sesefras Magna", Description = "A giant gas planet orbiting the star Zondostina in the Pleiades system. It is orbited by the small, blue moon of Epun, and the space near the planet is home to the space station Port Sesefron.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Sqornshellous Zeta", Description = "A swampy planet, found in the Sqornshellous system, best known for its mattresses. It is a very swampy planet, often covered in mist. Its mattresses seem to be used all over the Universe.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Traal", Description = "The homeworld of the Ravenous Bugblatter Beast of Traal.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Ursa Minor Beta", Description = "A rich and sunny planet and one of the most popular holiday destinations in the Universe. It is always Saturday afternoon on this planet, just before the beach bars close.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Vogsphere", Description = "The homeworld of the Vogons. It is also the home of Scintillating Jewelled Scuttling Crabs, and is near the Vogsol star.", Capacity = rand.Next(10, 42) },
                new Destination { Name = "Xaxrax Sigma", Description = "A planet in Sector XXXZ5QZX on which The Evildrome Boozarama resides, which is seen as one of the best places to get a Pan Galactic Gargle Blaster. One can expect to pay 75 Altairian dollars for a Pan Galactic Gargle Blaster on this planet.", Capacity = rand.Next(10, 42) },
            };
            foreach (Destination destination in destinations)
            {
                context.Destinations.Add(destination);
            }
            context.SaveChanges();

            // všechny možné termíny dle zadání
            var dates = new List<DateTime>();
            var firstDate = DateTime.Parse("2020-06-01");
            for (var i = 0; i < 85; i++)
            {
                dates.Add(firstDate.AddDays(i));
            }

            // 10 až 20 náhodných termínů pro každou destinaci
            foreach (Destination destination in destinations)
            {
                var datesFrom = dates.OrderBy(date => rand.Next()).Take(rand.Next(10, 21));
                foreach (DateTime dateFrom in datesFrom)
                {
                    context.Timeslots.Add(new Timeslot { DestinationID = destination.ID, DateFrom = dateFrom, DateTo = dateFrom.AddDays(7) });
                }
            }
            context.SaveChanges();

            // Ursa Minor Beta je populární, takže už je obsazeno
            var ursaMinorBeta = destinations.Single(d => d.Name == "Ursa Minor Beta");
            foreach (Timeslot timeslot in ursaMinorBeta.Timeslots)
            {
                context.Orders.Add(new Order { TimeslotID = timeslot.ID, CreatedAt = DateTime.Now, Name = "Duck", Surname = "Agency", Email = "duck@duckagency.com", Phone = "+845 428 741 937", Attendees = ursaMinorBeta.Capacity });
            }
            context.SaveChanges();

            // Sqornshellous Zeta má už skoro obsazeno, v každém termínu zbývá posledních 2 až 8 míst
            var sqornshellousZeta = destinations.Single(d => d.Name == "Sqornshellous Zeta");
            foreach (Timeslot timeslot in sqornshellousZeta.Timeslots)
            {
                context.Orders.Add(new Order { TimeslotID = timeslot.ID, CreatedAt = DateTime.Now, Name = "Duck", Surname = "Agency", Email = "duck@duckagency.com", Phone = "+845 428 741 937", Attendees = sqornshellousZeta.Capacity - rand.Next(2, 9) });
            }
            context.SaveChanges();
        }
    }
}
