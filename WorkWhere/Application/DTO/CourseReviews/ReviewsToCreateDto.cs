using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.CourseReviews
{
    public class ReviewsToCreateDto
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public int Rating { get; set; }
        public string Review { get; set; }
    }
}
