using Application.DTO._ٌRoomDtos;
using Application.Interfaces.RoomInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WorkWhere.Controllers.RoomModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        public BookingController(IBookingService bookingService)
        {
            BookingService = bookingService;
        }

        public IBookingService BookingService { get; }
        [HttpPost]
        public ActionResult AddBooking(BookingDto bookingDto)
        {
            BookingService.AddBooking(bookingDto);
            return Ok();
        }
    }
}
