using Application.DTO.CourseTableSlot;
using Core.Entities;
using Microsoft.AspNetCore.Http;
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
        public string Name { get; set; }
        public IFormFile Photo { get; set; }
       // public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string Location { get; set; }
        public int TeacherId { get; set; }

        public   CourseScheduleToCreateDto CourseSchedule {  get; set; }
      }  
}
