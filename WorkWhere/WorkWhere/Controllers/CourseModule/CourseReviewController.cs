using Core.Entities;
using Core.Infrastructure.Contract.Course;
using Microsoft.AspNetCore.Mvc;

namespace WorkWhere.Controllers.CourseModule
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseReviewController : ControllerBase
    {
        private readonly ICourseReviewRepository _repository;

        public CourseReviewController(ICourseReviewRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseReview>>> GetAllCourseReviews()
        {
            var courseReviews = await _repository.GetAllCourseReviewsAsync();
            return Ok(courseReviews);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseReview>> GetCourseReviewByIdAsync(int courseId, int id)
        {
            var courseReview = await _repository.GetCourseReviewByIdAsync(courseId, id);

            if (courseReview == null)
            {
                return NotFound(); // Return 404 Not Found response
            }

            return Ok(courseReview); // Return 200 OK response with the courseReview
        }

        #region لسسسسسسسسسسسسسسسسسسسسسسسسسسسسسسسسسهههههههه

        [HttpPost]
        public async Task<IActionResult> AddCourseReview(CourseReview courseReview)
        {
            await _repository.AddCourseReviewAsync(courseReview);
            return CreatedAtAction(nameof(GetCourseReviewByIdAsync), new { id = courseReview.Id }, courseReview);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourseReview(int id)
        {
            try
            {
                await _repository.DeleteCourseReviewAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        } 
        #endregion
    }
}
