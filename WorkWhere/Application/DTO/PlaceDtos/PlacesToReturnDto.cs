using Application.DTO._ٌRoomDtos;
using Application.DTO.PlaceReviewDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PlaceDtos
{
    public class PlacesToReturnDto:PlaceDto
    {
        public int Id { get; set; }


        public List<string> PlacePhotos { get; set; }
        
    }
}
