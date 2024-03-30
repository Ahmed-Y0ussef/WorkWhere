using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Course
{
    public class courseToUpdateDto
    {
        public decimal Price { get; set; } // Only include properties that can be updated
        public int Capacity { get; set; }
        public string PictureUrl { get; set; }
        public bool IsInPlace { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
    }
}
