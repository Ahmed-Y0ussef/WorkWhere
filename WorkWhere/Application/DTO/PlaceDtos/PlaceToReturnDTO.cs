using Core.Entities;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Application.DTO._ٌRoomDtos;
using Application.DTO.PlaceReviewDtos;

namespace Application.DTO.PlaceDtos
{
    public class PlaceToReturnDTO:PlaceDto
    {
     
        public int Id { get; set; }
       

        public List<string> PlacePhotos { get; set; }
        public List<RoomToReturnWithPlaceDto> Rooms { get; set; }
        public List<PlaceReviewToReturnDto> Reviews { get; set; }
    }
}
