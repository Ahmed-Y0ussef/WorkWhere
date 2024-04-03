using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.CourseSchedule
{
    public class CourseScheduleToReturnDto
    {
        
        public string[] Dates { get; set; }

        public string startHour { get; set; }

        public string endHour { get; set; }

    }
}
