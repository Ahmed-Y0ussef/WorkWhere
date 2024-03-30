using Application.DTO.Course;
using Core.Entities;
using Core.Infrastructure.Contract;
using Infrastructure.Dbcontext;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WorkWhere.Controllers.CourseModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        //private readonly GenericRepository<Course> _repository;

        //public CourseController(GenericRepository<Course> repository)
        //{
        //    _repository = repository;
        //}

        private readonly IUnitofwork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public CourseController(IUnitofwork unitOfWork, ApplicationDbContext context)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        // GET: api/course
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _unitOfWork.GetRepository<Course>().GetAllAsync();
            return Ok(courses);
        }

        // GET: api/course/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _unitOfWork.GetRepository<Course>().GetAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return Ok(course);
        }

        //[HttpPost]
        //public async Task<ActionResult<Course>> CreateCourse([FromBody] Course course)
        //{
        //    // Validation for required fields and data integrity
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    await _unitOfWork.GetRepository<Course>().CreateAsync(course);
        //    await _unitOfWork.CommitAsync();

        //    return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        //}

        //[HttpPost] ✅
        //public async Task<ActionResult> CreateCourse([FromBody] courseToCreateDto courseDto)
        //{
        //    // Validate the DTO
        //    if (!ModelState.IsValid)
        //    {
        //        // If validation fails, return a bad request response with error details
        //        return BadRequest(ModelState);
        //    }

        //    // Map CourseCreateDto to Course entity
        //    var course = new Course
        //    {
        //        Name = courseDto.Name,
        //        PictureUrl = courseDto.PictureUrl,
        //        //Num_Of_Students_Joined = courseDto.Num_Of_Students_Joined,
        //        Price = courseDto.Price,
        //        Capacity = courseDto.Capacity,
        //        //IsInPlace = courseDto.IsInPlace,
        //        Location = courseDto.Location,
        //        //AdminId = courseDto.AdminId,
        //        TeacherId = courseDto.TeacherId
        //    };

        //    // Save course to the database asynchronously
        //    await _unitOfWork.GetRepository<Course>().CreateAsync(course);
        //    await _unitOfWork.CommitAsync();

        //    // Return a success response
        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> CreateCourse(courseToCreateDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request with validation errors
            }


            //var teacher = await _unitOfWork.GetRepositoryTwo<UserRepository>()
            //                        .GetUserByIdWithRoleAsync(courseDto.TeacherId, "Teacher");


            //اوعى المكرونة
            var teacher = await _context.Users
                       .Where(u => u.Id == courseDto.TeacherId
                                 && u.Roles.Any(r => r.Name == "Teacher"))
                       .FirstOrDefaultAsync();


            if (teacher == null)
            {
                return BadRequest("Teacher not found or doesn't have the 'Teacher' role.");
            }


            var course = new Course
            {
                Name = courseDto.Name,
                PictureUrl = courseDto.PictureUrl,
                Capacity = courseDto.Capacity,
                Location = courseDto.Location,
                TeacherId = courseDto.TeacherId,
                IsInPlace = false // Set default value
                , AdminId = 1 
                , Num_Of_Students_Joined = 0
            };

            // Assign teacher
           // course.Teacher = teacher;

            // Save the course
            await _unitOfWork.GetRepository<Course>().CreateAsync(course);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course); 
            // Return 201 Created with the new course
        }


            [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, courseToUpdateDto courseUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var courseFromDb = await _unitOfWork.GetRepository<Course>().GetAsync(id);

            if (courseFromDb == null)
            {
                return NotFound();
            }
            courseFromDb.Price = courseUpdateDto.Price;
            courseFromDb.Capacity = courseUpdateDto.Capacity;
            courseFromDb.PictureUrl = courseUpdateDto.PictureUrl;
            courseFromDb.IsInPlace = courseUpdateDto.IsInPlace;
            courseFromDb.Location = courseUpdateDto.Location;
            courseFromDb.Name = courseUpdateDto.Name;

            await _unitOfWork.GetRepository<Course>().UpdateAsync(courseFromDb);
            await _unitOfWork.CommitAsync();

            return NoContent(); 
        }

        // DELETE: api/course/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _unitOfWork.GetRepository<Course>().GetAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            await _unitOfWork.GetRepository<Course>().DeleteAsync(course);

            return NoContent();
        }
    }
}
