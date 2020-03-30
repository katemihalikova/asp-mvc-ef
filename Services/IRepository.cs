using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ASP1.Models;

namespace ASP1.Services
{
    public interface IRepository
    {
        Task<IEnumerable<Destination>> GetDestinations();
        Task<Destination> GetDestinationByID(int destinationID);
        Task<IEnumerable<Timeslot>> GetTimeslots();
        Task<Timeslot> GetTimeslotByID(int timeslotID);
        void InsertOrder(Order order);
        Task<int> Save();
    }
}
