using Application.DTO.CourseSchedule;
using Application.DTO.CourseTableSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Course
{
    public class PendingCoursesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public int Capacity { get; set; }

        public string status { get; set; }
        public string TeacherName { get; set; }

        public CourseScheduleToReturnDto CourseSchedule { get; set; }
    }
}
