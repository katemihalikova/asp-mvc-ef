using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP1.Models
{
    public class Destination
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }

        public virtual ICollection<Timeslot> Timeslots { get; set; }

        public bool CanOrder
        {
            get => Timeslots.Any(t => t.FreeCapacity > 0);
        }
    }
}
