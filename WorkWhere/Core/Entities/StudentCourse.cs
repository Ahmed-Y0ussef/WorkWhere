using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class StudentCourse
    {
        public string StdId { get; set; }
        public int CrsId { get; set; }
        public User  User { get; set; }
        public Course Course { get; set; }
        
    }
}
