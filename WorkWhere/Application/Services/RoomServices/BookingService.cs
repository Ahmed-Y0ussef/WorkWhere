using Application.DTO._ٌRoomDtos;
using Application.Interfaces.RoomInterfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.RoomServices
{
    public class BookingService : IBookingService
    {
        public IBookingRepo BookingRepo { get; }
        public IMapper Mapper { get; }

        public BookingService(IBookingRepo bookingRepo,IMapper mapper)
        {
            BookingRepo = bookingRepo;
            Mapper = mapper;
        }


        public async Task AddBooking(BookingDto bookingDto)
        {
            await BookingRepo.AddBooking(Mapper.Map<BookingDto,RoomBooking>(bookingDto));
        }

        public Task<IEnumerable<BookingDto>> GetBookingTimeSlots()
        {
            throw new NotImplementedException();
        }
    }
}
