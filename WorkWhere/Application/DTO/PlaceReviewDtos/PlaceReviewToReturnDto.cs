using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.PlaceReviewDtos
{
    public class PlaceReviewToReturnDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string User { get; set; }

    }
}
