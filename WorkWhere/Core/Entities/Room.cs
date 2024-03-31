using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Room:BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; } = null;
        public Status? Status { get; set; }
        public int Capacity {  get; set; }
        public decimal PricePerHour { get; set; }
        public int? AdminId { get; set; }
        public int? GuestId { get; set; }
        public int PlaceId { get; set; }

        //Nav Prop
        public Place Place { get; set; }
        public User Admin { get; set; }
        public ICollection<GuestRoom> GuestRooms { get; set; } = new HashSet<GuestRoom>();
        public ICollection<RoomUtilities> RoomUtilities { get; set; }

        public ICollection<RoomReview> RoomReviews { get; set;} = new HashSet<RoomReview>();
        public ICollection<RoomPhotos> RoomPhotos { get; set; } = new HashSet<RoomPhotos>();
        public ICollection<RoomTimeSlot> RoomTimeSlots { get; set;} = new HashSet<RoomTimeSlot>();

        //****************************************************************************************
        //public ICollection <RoomSchedule> RoomSchedules { get; set;} = new HashSet<RoomSchedule>();

    }
}
