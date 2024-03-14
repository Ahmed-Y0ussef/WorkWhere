using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class CourseTableSlot
    {
        public DateTime Date {  get; set; }
        public TimeSpan StratHour {  get; set; }
        public TimeSpan EndHour {  get; set; }
        public int CourseId {  get; set; }
        public Course Course { get; set; }
    }
}
