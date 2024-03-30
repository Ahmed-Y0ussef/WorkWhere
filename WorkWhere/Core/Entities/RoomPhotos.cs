using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RoomPhotos
    {
        // public byte[] photo { get; set; }
        public string PictureUrl { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
