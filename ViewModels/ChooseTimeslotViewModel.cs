using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP1.Models;
using System.ComponentModel.DataAnnotations;

namespace ASP1.ViewModels
{
    public class ChooseTimeslotViewModel
    {
        public Destination Destination { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [RegularExpression(@"2020-0([6-8]-(0[1-9]|[12][0-9]|30)|[78]-31)$", ErrorMessage = "Value for {0} must be between 1.6. and 31.8.2020.")]
        [Display(Name = "From")]
        public string DateFrom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [RegularExpression(@"2020-0([6-8]-(0[1-9]|[12][0-9]|30)|[78]-31)$", ErrorMessage = "Value for {0} must be between 1.6. and 31.8.2020.")]
        [Display(Name = "To")]
        public string DateTo { get; set; }

        [Required]
        [Range(1, double.PositiveInfinity)]
        [Display(Name = "Number of persons")]
        public int Attendees { get; set; }

        public int TimeslotID { get; set; }
    }
}
