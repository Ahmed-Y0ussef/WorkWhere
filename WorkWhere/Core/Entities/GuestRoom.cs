using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class GuestRoom
    {
        public int GuestId { get; set; }
        public int RoomId { get; set; }

        public Room Room { get; set; }
        public User Guest { get; set; }
    }
}
