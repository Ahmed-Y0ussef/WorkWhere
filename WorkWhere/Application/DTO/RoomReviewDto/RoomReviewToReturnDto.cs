using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.RoomReviewDto
{
    public class RoomReviewToReturnDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string User { get; set; }
    }
}
