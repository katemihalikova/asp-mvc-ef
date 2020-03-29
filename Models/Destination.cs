using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASP1.Models
{
    public class Destination
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 50)]
        public string Description { get; set; }

        [Required]
        [Range(1, double.PositiveInfinity)]
        public int Capacity { get; set; }

        public virtual ICollection<Timeslot> Timeslots { get; set; }

        public bool CanOrder
        {
            get => Timeslots.Any(t => t.FreeCapacity > 0);
        }
    }
}
