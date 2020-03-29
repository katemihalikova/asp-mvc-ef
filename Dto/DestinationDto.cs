using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP1.Dto
{
    public class DestinationDto
    {
        public int id;
        public string name;
        public string description;
        public int capacity;
        public IEnumerable<TimeslotDto> timeslots;
    }
}
