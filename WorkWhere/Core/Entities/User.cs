using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Core.Entities
{
    public class User : IdentityUser
    {

        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "First name must be at least {2}, and maximum {1} character")]
        public string FirstName { get; set; }
        [Required, StringLength(15, MinimumLength = 3, ErrorMessage = "Last name must be at least {2}, and maximum {1} character")]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; } = false;


        [Required]
        public byte[] PersonalImg { get; set; }

        [Required]
        public byte[] NImg { get; set; }
        [Required, StringLength(14, MinimumLength = 14)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        public string NId { get; set; }

        [Required, StringLength(11, MinimumLength = 11)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Only numbers are allowed.")]
        public string PhoneNumber { get; set; }

        //public string? AdminID { get; set; }
        //public Status? Status { get; set; }



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
        //public User Admin {  get; set; }

        // with Guestroom
        public ICollection<GuestRoom> GuestRooms { get; set;} = new HashSet<GuestRoom>();
        public ICollection<Contact> Conacts { get; set; }
        public ICollection<CourseReview> courseReviews { get; set; }
        public ICollection<PlaceReview> placeReviews { get; set; }  
        public ICollection<RoomReview> roomReviews { get; set; }
    }
}
