using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO._ٌRoomDtos
{
    public class RoomToReturnDto:RoomToReturnWithPlaceDto
    {
        public string Place {  get; set; }
        public List<string> RoomReviews { get; set; }

    }
}
