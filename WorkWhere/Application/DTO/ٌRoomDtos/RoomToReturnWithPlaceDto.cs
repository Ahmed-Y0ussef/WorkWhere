using Application.DTO.RoomTimeSlot;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO._ٌRoomDtos
{
    public class RoomToReturnWithPlaceDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }

        public List<string> RoomUtilities { get; set; }
      //  public List<RoomTimeSlotToReturn> TimeSlots { get; set; }

        public List<string>? RoomPhotos { get; set; }
    }
}
