using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASP1.Models
{
    public class Timeslot
    {
        public int ID { get; set; }

        [Required]
        public int DestinationID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public virtual Destination Destination { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public int FreeCapacity
        {
            get => Destination.Capacity - Orders.Sum(o => o.Attendees);
        }
    }
}
