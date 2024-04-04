using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO._ٌRoomDtos
{
    public class RoomToCreateUpdate:RoomCreateWithPlaceDto
    {
        public int PlaceId { get; set; }
    }
}
