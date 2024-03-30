using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Course
{
    public class courseToReturnDtos
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string PictureUrl { get; set; }
        public bool IsInPlace { get; set; }
        public int Num_of_Students_Joined { get; set; }
        public string Location { get; set; }
        public string Name { get; set; }
    }
}
