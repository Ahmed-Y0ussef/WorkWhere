using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.CourseReviews
{
    public class ReviewsToReturnDto
    {

        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
        public int courseId { get; set; }
        public string courseName { get; set; }
        public string userName { get; set; }
    }
}
