using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PlaceReviewDtos
{
    public class PlaceReviewToCreateDto
    {
        public int? UserId { get; set; }
        public string? Description { get; set; }

        public int PlaceId { get; set; }
        public int? Rating { get; set; }

    }
}
