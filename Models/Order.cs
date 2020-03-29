using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ASP1.Models
{
    public class Order
    {
        public int ID { get; set; }

        [Required]
        public int TimeslotID { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\+\d{3}( \d{3}){3}$", ErrorMessage = "{0} must have format +XXX XXX XXX XXX.")]
        public string Phone { get; set; }

        [Required]
        [Range(1, double.PositiveInfinity)]
        public int Attendees { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public virtual Timeslot Timeslot { get; set; }
    }
}
