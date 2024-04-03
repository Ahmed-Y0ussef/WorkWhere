using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class PlaceReview:BaseEntity
    {
        //public int Id { get; set; }
        public string? UserId { get; set; }

        public int PlaceId { get; set; }
        public int? Rating { get; set; }
       // public string? Review { get; set; }
        public Place Place { get; set; }
        public User User { get; set; }
    }
}
