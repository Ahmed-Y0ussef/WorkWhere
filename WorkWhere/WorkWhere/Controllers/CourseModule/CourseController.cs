using Application.DTO.Course;
using Application.DTO.CourseReviews;
using Application.Helpers.ResponseResults;
using Application.Services;
using Azure.Core;
using Core;
using Core.Application.Contract;
using Core.Entities;
using Core.Infrastructure.Contract;
using Infrastructure.Dbcontext;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;

namespace WorkWhere.Controllers.CourseModule
{

    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {

            _courseService = courseService;
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(/*[FromBody] */courseToCreateDto courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _courseService.CreateCourseAsync(courseDto);
            return Ok(new JsonResult(new { title = "Course Added", message = "Course created successfully!" }));
        }

        [HttpPut/*("{id}")*/]
        public async Task<IActionResult> UpdateCourse(courseToUpdateDto courseDto)
        {
            var course = await _courseService.GetCourseAsync(courseDto.courseId);

            if (course == null)
            {
                return BadRequest(new JsonResult(new { title = "Course Not Found", message = "There is no course with this id" }));

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _courseService.UpdateCourseAsync(/*id, */courseDto);

            return Ok(new JsonResult(new { title = "Course Updated", message = "Course updated successfully!" }));

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _courseService.GetCourseAsync(id);

            if (course == null)
            {
                return NotFound(new JsonResult(new { title = "Course Not Found", message = "Course Not found" }));

            }

            return Ok(course);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {

            var course = await _courseService.GetCourseAsync(id);

            if (course == null)
            {
                 return NotFound(new JsonResult(new { title = "NotFound", message = "Course Not Found!" }));
            }
            await _courseService.DeleteCourseAsync(id);

            var updatedCourses = await _courseService.GetCoursesAsync(); 

            return Ok(updatedCourses);
        }

        [HttpGet]
        [Route("/accepted-courses")]
        public async Task<IActionResult> GetAcceptedCourses()
        {
            var AcceptedCourses = await _courseService.GetAcceptedCoursesAsync();

            if (AcceptedCourses == null || !AcceptedCourses.Any())
            {
                return Ok(new JsonResult(new { title = "Accepted Courses", message = "There are no Accepted courses." }));
            }
            return Ok(AcceptedCourses);
        }


        //***********************************  Reviews  ************************************

        [HttpGet]
        [Route("/course-reviews")]
        public async Task<IActionResult> GetCourseReviews(int courseid)
        {
            #region MyRegion
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //int courseId = courseid;


            //var course = await _context.Courses
            // .Include(c => c.CoursesReviews) 
            // .ThenInclude(r => r.User)     
            // .FirstOrDefaultAsync(c => c.Id == courseId);

            //if (course == null)
            //{
            //    return NotFound("Course not found.");
            //}

            //var courseReviews = await _context.courseReviews
            //    .Where(cr => cr.CourseId == courseId)
            //     .ToListAsync();

            //var reviews = courseReviews.Select(cr => new ReviewsToReturnDto
            //{
            //    Rating = cr.Rating,
            //    Review = cr.Review,
            //    userName = cr.User.Name,
            //    courseName = cr.Course.Name
            //}).ToList(); 
            #endregion

            var reviews = await _courseService.GetCourseReviewsAsync(courseid);

            if (reviews == null || !reviews.Any())
            {
                return NotFound(new JsonResult(new { title = "No Reviews", message = "There is no reviews for this course" }));

            }
            return Ok(reviews);
        }

        [HttpPost]
        [Route("/review")]
        public async Task<IActionResult> AddReview( ReviewsToCreateDto reviewDto)
        {
            #region MyRegion
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //int courseId = courseid;
            // var course = await _context.Courses.FindAsync(courseId);

            //if (course == null)
            //{
            //    return NotFound("Course not found.");
            //}

            //var newReview = new CourseReview
            //{
            //    UserId = reviewDto.UserId, // token
            //    CourseId = courseId,
            //    Rating = reviewDto.Rating,
            //    Review = reviewDto.Review
            //};

            //course.CoursesReviews.Add(newReview);
            //await _context.SaveChangesAsync(); 
            #endregion

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var course = await _courseService.GetCourseAsync(reviewDto.CourseId);

            if (course == null)
            {
                return BadRequest(new JsonResult(new { title = "Course Not Found", message = "This course not found to add review" }));

            }


            await _courseService.AddReviewAsync(reviewDto);

            return Ok(new JsonResult(new { title = "Review Added", message = "Review added successfully!" }));
        }

        [HttpDelete]
        [Route("/review")]
        public async Task<IActionResult> DeleteReview(ReviewToDeleteDto reviewToDeleteDto)
        {
            #region MyRegion

            //int courseId = courseid;
            //var course = await _context.Courses.FindAsync(courseId);

            //int reviewId = reviewid;
            //var review = await _context.courseReviews
            // .FirstOrDefaultAsync(r => r.CourseId == courseId && r.Id == reviewId);


            //if (review == null)
            //{
            //    return NotFound("Review not found.");
            //}

            //_context.courseReviews.Remove(review);
            //await _context.SaveChangesAsync(); 
            #endregion

            await _courseService.DeleteReviewAsync(reviewToDeleteDto);
            return Ok(new JsonResult(new { title = "Review Deleted", message = "Review deleted successfully!" }));
        }


        //******************************** JOINING  & CANCELLING ************************
        [HttpPost]
        [Route("/join-course")]
        public async Task<IActionResult> JoinCourse(coursesToJoinDto coursesToJoinDto)
        {
            #region MyRegion
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //var course = await _context.Courses.Include(c => c.StudentsCourses).FirstOrDefaultAsync(c => c.Id == courseId);

            //if (course == null)
            //{
            //    return NotFound();
            //}
            //var isEnrolled = _context.studentCourses.Any(sc => sc.CrsId == courseId && sc.StdId == userId); //token
            //if (isEnrolled)
            //{
            //    return BadRequest("Student already enrolled in this course.");
            //}

            //if (course.Num_Of_Students_Joined >= course.Capacity)
            //{
            //    return BadRequest("There is no empty seats, Course is full.");
            //}

            //var studentCourse = new StudentCourse
            //{
            //    CrsId = courseId,
            //    StdId = userId,//token 
            //};

            //course.Num_Of_Students_Joined++;

            //_context.studentCourses.Add(studentCourse);
            //await _context.SaveChangesAsync();

            //return Ok("You Are Enrolled in the course NOW!");
            #endregion

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            #region MyRegion
            var result = await _courseService.JoinCourseAsync(coursesToJoinDto);

            switch (result)
            {
                case JoinCourseResult.Success:
                    return Ok(new JsonResult(new { title = "Student Joined", message = "You are enrolled in the course NOW!" }));
                case JoinCourseResult.InvalidModelState:
                    return BadRequest(ModelState);
                case JoinCourseResult.CourseNotFound:
                    return NotFound(new JsonResult(new { title = "Course Not Found", message = "Course Not found" } ));
                case JoinCourseResult.AlreadyEnrolled:
                   return BadRequest(new JsonResult(new { title = "Cannot Join", message = "Student already enrolled in this course." }));
                case JoinCourseResult.CourseFull:
                    return BadRequest(new JsonResult(new { title = "Cannot Join", message = "There are no empty seats. The course is full." }));
                default:
                    return StatusCode(500, "An unexpected error occurred.");

            }
            #endregion
        }

        [HttpDelete]
        [Route("/cancel-course")]
        public async Task<IActionResult> CancelCourse(coursesToJoinDto coursesToJoinDto)
        {
            #region MyRegion
            //if (courseId <= 0 || userId <= 0)
            //{
            //    return BadRequest("Invalid course or student ID.");
            //}

            //var joinedStudent = await _context.studentCourses
            //  .FirstOrDefaultAsync(sc => sc.CrsId == courseId && sc.StdId == userId);

            //if (joinedStudent == null)
            //{
            //    return NotFound("Student is not enrolled in this course.");
            //}
            //_context.studentCourses.Remove(joinedStudent);
            //var course = await _context.Courses.FindAsync(courseId);
            //if (course != null && course.Num_Of_Students_Joined > 0)
            //{
            //    course.Num_Of_Students_Joined--;
            //}

            //await _context.SaveChangesAsync();

            //return Ok("Your enrollment is cancelled successfully."); 
            #endregion

            var result = await _courseService.CancelCourse(coursesToJoinDto);

            switch (result)
            {
                case CancelCourseResults.Success:
                    //  return Ok("Your enrollment is cancelled successfully.");
                    return Ok(new JsonResult(new { title = "Enrolled Cancelled", message = "Your enrollment is cancelled successfully." }));
                case CancelCourseResults.InvalidModelState:
                    return BadRequest(ModelState);
                case CancelCourseResults.NotEnrolled:
                    // return BadRequest("Student is not enrolled in this course");
                    return BadRequest(new JsonResult(new { title = "Cannot Cancel", message = "Student is not enrolled in this course" }));
                case CancelCourseResults.CourseOrStudentId:
                    // return BadRequest("Invalid course or student ID.");
                    return BadRequest(new JsonResult(new { title = "Cannot Cancel", message = "Invalid course or student ID." }));
                default:
                    return StatusCode(500, "An unexpected error occurred.");

            }
        }

        //************************************** Admin *********************************

     
        [HttpPut("/approve")]
        public async Task<IActionResult> ApproveCourse(coursesToManageDto coursesToManageDto)
        {

            #region MyRegion
            //var course = await _context.Courses.FindAsync(coursesToManageDto.CourseId);
            //if (course == null)
            //{
            //    return NotFound();
            //}

            //if (course.Status != Status.Pending)
            //{
            //    return BadRequest(new JsonResult(new { title = "Cannot Accept", message = "Course is already approved or rejected." }));

            //}

            //course.Status = Status.Accepted;
            //course.AdminId = coursesToManageDto.AdminId;

            //await _context.SaveChangesAsync(); 


            //return Ok(new JsonResult(new { title = "Course Approved", message = "Course Approved successfully!" }));

            #endregion

            var approvalResult = await _courseService.ApproveCourse(coursesToManageDto);

            switch (approvalResult)
            {
                case ManageCoursesResults.CourseNotFound:
                    return NotFound("Course not found.");
                case ManageCoursesResults.AllreadyApproved:
                    return BadRequest("Course is already approved.");
                case ManageCoursesResults.Success:
                    return Ok("Course approved successfully.");
                default:
                    return StatusCode(500, "Unexpected error occurred.");
            }
        }
       
        [HttpPut("/reject")]
        public async Task<IActionResult> RejectCourse(coursesToManageDto coursesToManageDto)
        {
            #region MyRegion
            //var course = await _context.Courses.FindAsync(coursesToManageDto.CourseId);
            //if (course == null)
            //{
            //    return NotFound();
            //}

            //if (course.Status != Status.Pending)
            //{
            //    return BadRequest(new JsonResult(new { title = "Cannot Reject", message = "Course is already approved." }));

            //}

            //course.Status = Status.Refused; 

            //_context.Courses.Remove(course);

            //await _context.SaveChangesAsync();

            //return Ok(new JsonResult(new { title = "Course Rejected", message = "Course Rejected & Deleted successfully!" }));


            #endregion

            var rejectionResult = await _courseService.RejectCourse(coursesToManageDto);

            switch (rejectionResult)
            {
                case ManageCoursesResults.CourseNotFound:
                    return NotFound("Course not found.");
                case ManageCoursesResults.CannotReject:
                    return BadRequest("Course is already approved or rejected.");
                case ManageCoursesResults.Success:
                    return Ok("Course rejected and deleted successfully.");
                case ManageCoursesResults.Error:
                    return StatusCode(500, "Unexpected error occurred.");
                default:
                    return StatusCode(500, "Unexpected result.");
            }

        }

        [HttpGet]
        [Route("/pending")]
        public async Task<IActionResult> GetPendingCourses()
        {
            var pendingCourses = await _courseService.GetPendingCoursesAsync();

            if (pendingCourses == null || !pendingCourses.Any())
            {
                return Ok(new JsonResult(new { title = "Pending Courses", message = "There are no pending courses." }));

            }
            return Ok(pendingCourses);
        }

        [HttpGet]
        [Route("/enrolled-students")]
        public async Task<IActionResult> GetEnrolledStudents(int courseid)
        {
            
            var course = await _courseService.GetCourseAsync(courseid);
            if (course == null)
            {
                return BadRequest(new JsonResult(new { title = "Course Not Found", message = "There is no course with this id" }));
            }

            var enrolledStudents = await _courseService.GetEnrolledStudentsAsync(courseid);

            if (enrolledStudents == null || !enrolledStudents.Any())
            {
                return NotFound(new JsonResult(new { title = "No Students", message = "There is no students enrolled in this course" }));

            }
            return Ok(enrolledStudents);
        }

        [HttpGet]
        [Route("/all-enrolled-students")]
        public async Task<IActionResult> GetAllStudents()
        {

            var students = await _courseService.GetAllStudentsAsync();

            if (students == null || !students.Any())
            {
                return NotFound(new JsonResult(new { title = "No students", message = "There is no students Available" }));

            }
            return Ok(students);
        }

        [HttpGet]
        [Route("/empty-courses")]
        public async Task<IActionResult> GetEmptyCourses()
        {
            var courses = await _courseService.GetEmptyCoursesAsync();

            if (courses == null || !courses.Any())
            {
                return Ok(new JsonResult(new { title = "No Courses", message = "There is no empty courses" }));

            }
            return Ok(courses);
        }

        [HttpGet]
        [Route("/all-reviews")]
        public async Task<IActionResult> GetCoursesReviews()
        {
            #region MyRegion
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            //int courseId = courseid;


            //var course = await _context.Courses
            // .Include(c => c.CoursesReviews) 
            // .ThenInclude(r => r.User)     
            // .FirstOrDefaultAsync(c => c.Id == courseId);

            //if (course == null)
            //{
            //    return NotFound("Course not found.");
            //}

            //var courseReviews = await _context.courseReviews
            //    .Where(cr => cr.CourseId == courseId)
            //     .ToListAsync();

            //var reviews = courseReviews.Select(cr => new ReviewsToReturnDto
            //{
            //    Rating = cr.Rating,
            //    Review = cr.Review,
            //    userName = cr.User.Name,
            //    courseName = cr.Course.Name
            //}).ToList(); 
            #endregion

            var reviews = await _courseService.GetAllCoursesReviewsAsync();

            if (reviews == null || !reviews.Any())
            {
                return NotFound(new JsonResult(new { title = "No Reviews", message = "There is no reviews Available" }));

            }
            return Ok(reviews);
        }


        [HttpGet]
        [Route("/all-courses")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetCoursesAsync();

            if (courses == null)
            {
                return NotFound(new JsonResult(new { title = "No Courses", message = "There is no Courses to show" }));

            }

            return Ok(courses);
        }


        //************************************************************************

        [HttpGet]
        [Route("/enrolled-courses")]
        public async Task<IActionResult> GetEnrolledCourses(int studentId)
        {
            var enrolledCoursesDto = await _courseService.GetEnrolledCoursesAsync(studentId);

            if (enrolledCoursesDto == null)
            {

                return Ok(new JsonResult(new { title = "No Courses", message = "You didn't enroll any courses" }));

            }

            // Do something with the enrolledCoursesDto, e.g., pass it to the view
            return Ok(enrolledCoursesDto);
        }

        [HttpGet]
        [Route("/taught-courses")]
        public async Task<IActionResult> GetTaughtedCourses(int teacherId)
        {
            var TaughtCourses = await _courseService.GetTaughtCoursesAsync(teacherId);

            if (TaughtCourses == null || !TaughtCourses.Any())
            {
                return Ok(new JsonResult(new { title = "No Courses", message = "You didn't add any courses" }));
            }
            return Ok(TaughtCourses);
        }


    }

}



