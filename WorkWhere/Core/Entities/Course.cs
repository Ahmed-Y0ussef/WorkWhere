using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Course:BaseEntity
    {
        [Required(ErrorMessage = "Course Name is required.")]
        [MaxLength(100, ErrorMessage = "Course Name must be less than 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Course Description is required.")]
        [MaxLength(500, ErrorMessage = "Course Description must be less than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Course Price is required.")]
        [Range(0.01, double.PositiveInfinity, ErrorMessage = "Course Price must be greater than or equal to $0.01.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Course Capacity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Course Capacity must be at least 1.")]
        public int Capacity { get; set; }
        public Status Status { get; set; } = Status.Pending;

        [Required(ErrorMessage = "Course Photo is required.")]
        public byte[] Photo { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Number of Enrolled Students cannot be negative.")]
        public int NumOfStudentsEnrolled { get; set; } = 0;

        [MaxLength(255, ErrorMessage = "Course Location must be less than 255 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Teacher ID is required.")]
        public int TeacherId{ get; set; }

        public int? AdminId { get; set; }

        //Nav PROP
        public User Teacher {  get; set; }
        public User Admin { get; set; }
        public ICollection<EnrolledStudents> EnrolledStudents { get; set; }= new HashSet<EnrolledStudents>();
        public ICollection<CourseReviews> CoursesReviews { get; set; } = new HashSet<CourseReviews>();

        public CourseSchedule CourseSchedule { get; set; }




    }
}
