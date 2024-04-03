using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.CourseTableSlot
{
    public class CourseScheduleToCreateDto
    {
        //public DateOnly StartDate { get; set; }
        //public DateOnly EndDate { get; set; }
        //public TimeSpanTicks StartHour { get; set; }
        //public TimeSpan StartHour { get; set; }

        //public TimeSpan EndHour { get; set; }
        //public List<DayOfWeek> DaysOff { get; set; }


        public string[] Dates { get; set; }

        public string startHour { get; set; }

        public string endHour { get; set; } 
        
    }
}
