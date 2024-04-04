using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.RoomReviewDto
{
    public class RoomReviewToCreateDto
    {
        public int? UserId { get; set; }
        public string? Description { get; set; }

        public int RoomId { get; set; }
        public int? Rating { get; set; }
    }
}
