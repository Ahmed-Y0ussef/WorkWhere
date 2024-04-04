using Application.DTO._ٌRoomDtos;
using Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PlaceDtos
{
    public class PlaceUpdateCreateDto:PlaceDto
    {
         public int? HostID { get; set; }
         //public List<RoomCreateWithPlaceDto>? Rooms { get; set; }
         public List<string>? PlacePhotos { get; set; }

    }
}
