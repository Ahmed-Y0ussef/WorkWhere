using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RoomReview
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int RoomId { get; set; }
        public int? Rating { get; set; }
        public string? Review { get; set; }
        public Room Room { get; set; }
        public User User { get; set; }
    }
}
