using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBookingRepo
    {
        public Task<IEnumerable<RoomBooking>> GetAvailableBooking();
        public Task AddBooking(RoomBooking roomBooking);


    }
}
