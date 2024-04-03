using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CourseSchedule : BaseEntity
    {
   

        public string[] Dates { get; set; }

        public string StartHour { get; set; }

        public string EndHour { get; set; }

        [ForeignKey(nameof(Course))]
        public int CourseId { get; set; }



       // public Course Course { get; set; }
    }
}
