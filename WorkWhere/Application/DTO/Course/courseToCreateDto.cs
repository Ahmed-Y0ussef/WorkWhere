using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Course
{
    public class courseToCreateDto
    {
        //[Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "Picture URL is required")]
        public string PictureUrl { get; set; }

        //[Required(ErrorMessage = "Number of Students Joined is required")]
        
        public decimal Price { get; set; }
        public int Capacity { get; set; }       
        public string Location { get; set; }
        public int TeacherId { get; set; }
    }
}
