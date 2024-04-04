using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class RoomBooking
    {
        public int Id { get; set; }
        public int GuestId { get; set; }
        public int RoomId { get; set; }
        public int PlaceId {  get; set; }
        public DateTime BookingDate { get; set; }
        public int StartTime {  get; set; }
        public int EndTime { get; set; } 
        public Place place {  get; set; }
        public Room Room { get; set; }
        public User Guest { get; set; }

        //public int TimeSlotId { get; set; }

        //public RoomTimeSlot TimeSlot { get; set; }
    }
}
