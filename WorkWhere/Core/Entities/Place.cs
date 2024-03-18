using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Place:BaseEntity
    {
        public string Name { get; set; }
        public Status? Status {  get; set; }
        public int NumOfRooms { get; set; }
        public double OpenTime {  get; set; }
        public double CloseTime { get; set; }
        public string City {  get; set; }
        public string StreetName { get; set; }
        public string ZonName { get; set; }
        public int BuildingNumber {  get; set; }
        public int? AdminId {  get; set; }
        public int HostID { get; set; }

        //nav Prop
        public ICollection<PlaceUtilities> PlaceUtilities { get; set; }
        public User Admin { get; set; }
        public User Host { get; set; }
        public ICollection<Room> Rooms { get; set; }
        public ICollection<PlaceReview> PlaceReviews { get; set; }

        public ICollection<PlacePhotos> PlacePhotos { get; set; }
       
    }
}
