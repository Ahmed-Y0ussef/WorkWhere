using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StudentCourse : BaseEntity
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public User  Student { get; set; }
        public Course Course { get; set; }
        
    }
}
