using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO._ٌRoomDtos
{
    public class BookingDto
    {
        public int RoomId { get; set; }
        public int PlaceId { get; set; }
        public DateTime BookingDate { get; set; }
        public int StartTime { get; set; }
        public int EndTime { get; set; }
       
    }
}
