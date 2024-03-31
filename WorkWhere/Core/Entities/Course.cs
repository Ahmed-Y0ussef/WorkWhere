using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Course:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
       
        public Status Status { get; set; } = Status.Pending;

        public byte[] Photo { get; set; }


        public int Num_Of_Students_Joined { get; set; } = 0;
        public string Location {  get; set; }
        public int? AdminId { get; set; }   
        public int TeacherId{ get; set; }

        //Nav PROP
        public User Teacher {  get; set; }
        public User Admin { get; set; }
        public ICollection<StudentCourse> StudentsCourses { get; set; }= new HashSet<StudentCourse>();
        public ICollection<CourseReview> CoursesReviews { get; set; } = new HashSet<CourseReview>();
        // public ICollection<CourseTableSlot> CoursesTableSlots { get; set; } = new HashSet<CourseTableSlot>();

        public CourseTableSlot CoursesTableSlot { get; set; }




    }
}
