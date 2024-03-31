using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User:BaseEntity
    {
       
        public string Name { get; set; }
        
        public string Email { get; set; }
        public string Password { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public string? Title {  get; set; }
       public string PersonalImageUrl { get; set; }
        [Required]
        public long? NId { get; set; }
        public string NIDUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public int? AdminID { get; set; }

        //NAV PROP
        //with place

        public ICollection<Place> PlacesOwned { get; set; }
        public ICollection<Place> PlacesAccepted { get; set; }

        //with StudentCourse
        public ICollection<StudentCourse> StudentCourses { get; set; } = new HashSet<StudentCourse>();
        
        //with Course
        public ICollection<Course> TaughtedCourses { get; set; } = new HashSet<Course>();
        public ICollection<Course> AcceptedCourses { get; set; } = new HashSet<Course>();
        // with User
        public User Admin {  get; set; }
        // with Roles
        public ICollection<Role> Roles { get; set; } = new HashSet<Role>();
        // with Guestroom
        public ICollection<GuestRoom> GuestRooms { get; set;} = new HashSet<GuestRoom>();
        public ICollection<Contact> Conacts { get; set; }
        public ICollection<CourseReview> courseReviews { get; set; }
        public ICollection<PlaceReview> placeReviews { get; set; }  
        public ICollection<RoomReview> roomReviews { get; set; }
    }
}
