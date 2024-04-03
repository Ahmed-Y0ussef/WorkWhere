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
        //public int CrsId {  get; set; }
        public string Name { get; set; }


        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public Status? Status { get; set; }
        public byte[] Photo { get; set; }
        public bool IsInPlace {  get; set; }
        public int? Num_Of_Students_Joined { get; set; }
        public string Location {  get; set; }
        public string? AdminId { get; set; }
        
        public string TeacherId{ get; set; }


        //Nav PROP
        public User Teacher {  get; set; }
        public User Admin { get; set; }
        public ICollection<StudentCourse> StudentsCourses { get; set; }= new HashSet<StudentCourse>();
        public ICollection<CourseReview> CoursesReviews { get; set; } = new HashSet<CourseReview>();
        public ICollection<CourseTableSlot> CoursesTableSlots { get; set; }

    }
}
