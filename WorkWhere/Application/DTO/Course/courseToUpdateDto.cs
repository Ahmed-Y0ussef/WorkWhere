using Application.DTO.CourseTableSlot;
using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Course
{
    public class courseToUpdateDto
    {

        public int courseId { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; } 
        public int? Capacity { get; set; }
        public string? Description { get; set; }
       // public IFormFile? Photo { get; set; }

        public string? Photo { get; set; }
        public string? Location { get; set; }

        //  public CourseScheduleToUpdateDto? CourseSchedule { get; set; }
        public string[]? Dates { get; set; }

        public string? startHour { get; set; }

        public string? endHour { get; set; }

        private bool _updateDates = false;  // Use a private backing field

        public bool UpdateDates => _updateDates;

        public void SetUpdateDatesFlag()
        {
            _updateDates = Dates != null && Dates.Length > 0;
        }





    }
}
