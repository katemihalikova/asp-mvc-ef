using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP1.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int TimeslotID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int Attendees { get; set; }
        public string Note { get; set; }

        public virtual Timeslot Timeslot { get; set; }
    }
}
