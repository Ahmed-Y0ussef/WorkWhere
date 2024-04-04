using Application.DTO._ٌRoomDtos;
using Application.DTO.RoomTimeSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RoomInterfaces
{
    public interface IBookingService
    {
        public Task<IEnumerable<BookingDto>> GetBookingTimeSlots();
        public Task AddBooking(BookingDto bookingDto);
    }
}
