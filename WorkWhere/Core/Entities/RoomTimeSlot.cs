using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RoomTimeSlot
    {
        public DateTime Date {  get; set; }
        public TimeSpan TimeStrart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        
    }
}
