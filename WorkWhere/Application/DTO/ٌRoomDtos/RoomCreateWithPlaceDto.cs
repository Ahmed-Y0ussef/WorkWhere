using Core.Entities;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Application.DTO.RoomTimeSlot;

namespace Application.DTO._ٌRoomDtos
{
    public class RoomCreateWithPlaceDto
    {
        public string? Description { get; set; }
        public string? Name { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerHour { get; set; }

        public List<string>? RoomUtilities { get; set; }
      //  public List<RoomTimeSlotCreateDto>? TimeSlots { get; set; }

        public List<string>? RoomPhotos { get; set; } 
    }
}
