using Core.Entities;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Application.DTO._ٌRoomDtos;

namespace Application.DTO.PlaceDtos
{
    public class PlaceDTO
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string Name { get; set; }
        public int NumOfRooms { get; set; }
        public double OpenTime { get; set; }
        public double CloseTime { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public string ZonName { get; set; }
        public int BuildingNumber { get; set; }
        public List<string> PlaceUtilities { get; set; }
       
        public List<RoomCreateDto> Rooms { get; set; }
        public List<string> PlaceReviews { get; set; }

        public List<string> PlacePhotos { get; set; }
    }
}
