using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP1.Models
{
    public class Timeslot
    {
        public int ID { get; set; }
        public int DestinationID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public virtual Destination Destination { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public int FreeCapacity
        {
            get => Destination.Capacity - Orders.Sum(o => o.Attendees);
        }
    }
}
