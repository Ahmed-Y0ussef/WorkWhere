//using Application.DTO.Course;
//using Core.Entities;
//using Core.Infrastructure.Contract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services
//{
//    public class CourseService
//    {

//        private readonly IUnitofwork _unitOfWork;

//        public CourseService(IUnitofwork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async Task<IEnumerable<Course>> GetCoursesAsync()
//        {
//            var courses = await _unitOfWork.GetRepository<Course>().GetAllAsync();
//            return courses;
//        }

//        public async Task<Course> GetCourseAsync(int id)
//        {
//            var course = await _unitOfWork.GetRepository<Course>().GetAsync(id);
//            return course;
//        }

//        public async Task<Course> CreateCourseAsync(CourseForCreationDto course)
//        {
//            // Validate the DTO (can be moved to a separate validation service)
//            if (!ModelState.IsValid)
//            {
//                throw new ArgumentException("Course DTO is invalid.");
//            }

//            // Map CourseForCreationDto to Course entity
//            var newCourse = new Course
//            {
//                Name = course.Name,
//                PictureUrl = course.PictureUrl,
//                Num_Of_Students_Joined = course.Num_Of_Students_Joined,
//                Price = course.Price,
//                Capacity = course.Capacity,
//                IsInPlace = course.IsInPlace,
//                Location = course.Location,
//                AdminId = course.AdminId,
//                TeacherId = course.TeacherId
//            };

//            // Save course to the database asynchronously
//            await _unitOfWork.GetRepository<Course>().CreateAsync(newCourse);
//            await _unitOfWork.CommitAsync();
//            return newCourse;
//        }

//        public async Task UpdateCourseAsync(int id, CourseForUpdateDto courseUpdateDto)
//        {
//            if (!ModelState.IsValid)
//            {
//                throw new ArgumentException("Course DTO is invalid.");
//            }

//            var courseFromDb = await _unitOfWork.GetRepository<Course>().GetAsync(id);

//            if (courseFromDb == null)
//            {
//                throw new KeyNotFoundException($"Course with ID {id} not found.");
//            }

//            courseFromDb.Price = courseUpdateDto.Price;
//            courseFromDb.Capacity = courseUpdateDto.Capacity;
//            courseFromDb.PictureUrl = courseUpdateDto.PictureUrl;
//            courseFromDb.IsInPlace = courseUpdateDto.IsInPlace;
//            courseFromDb.Location = courseUpdateDto.Location;
//            courseFromDb.Name = courseUpdateDto.Name;

//            await _unitOfWork.GetRepository<Course>().UpdateAsync(courseFromDb);
//            await _unitOfWork.CommitAsync();
//        }

//        public async Task DeleteCourseAsync(int id)
//        {
//            var course = await _unitOfWork.GetRepository<Course>().GetAsync(id);

//            if (course == null)
//            {
//                throw new KeyNotFoundException($"Course with ID {id} not found.");
//            }

//            await _unitOfWork.GetRepository<Course>().DeleteAsync(course);
//            await _unitOfWork.CommitAsync();
//        }
//    }
//}
