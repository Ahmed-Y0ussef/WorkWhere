using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Course
{
    public class CoursesEnrolledInDto
    {

        public int StudentId { get; set; }
        public IEnumerable<courseToReturnDto> EnrolledCourses { get; set; }
    }
}
