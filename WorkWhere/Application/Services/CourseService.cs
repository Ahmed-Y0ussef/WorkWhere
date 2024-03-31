using Application.DTO.Course;
using System.Linq;
using Application.Helpers;
using Application.DTO.CourseTableSlot;
using Core;
using Core.Application.Contract;
using Core.Entities;
using Core.Infrastructure.Contract;
using Infrastructure.Dbcontext;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.CourseReviews;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Application.Helpers.ResponseResults;


namespace Application.Services
{
    public class CourseService : ICourseService
    {

        private readonly IUnitofwork _unitOfWork;

        public CourseService(IUnitofwork unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
       
        public async Task<courseToReturnDto> GetCourseAsync(int id)
        {
            var course = await _unitOfWork.GetRepository<Course>()
                                        .GetAsync(id, c => c.Teacher, c => c.CoursesTableSlot);


            if (course == null)
            {
                
                return null; 
            }


            return new courseToReturnDto
            {
                Id= course.Id,
                Name = course.Name,
                Price = course.Price,
                Location = course.Location,
                Num_Of_Students_Joined = course.Num_Of_Students_Joined,
                Photo = course.Photo,
                Description = course.Description,
                Capacity = course.Capacity,
                status = course.Status.ToString(),
                TeacherName = course.Teacher.Name,
                //CourseReviews = course.CoursesReviews.Select(review => new ReviewsToReturnDto
                //{
                //    Rating = review.Rating,
                //    Review = review.Review,
                //    courseName = course.Name,
                //    userName = review.User?.Name
                //}).ToList(),

                //EnrolledStudents = course.StudentsCourses.Select(sc => new EnrolledStudentsDto
                //{
                //    StudentId = sc.StudentId,
                //    StudentName = sc.Student?.Name
                //}).ToList(),

                TableSlot = new TableSlotToReturn
                {

                    startHour = course.CoursesTableSlot?.StartHour,
                    endHour = course.CoursesTableSlot?.EndHour,
                    Dates = course.CoursesTableSlot?.Dates,
                } 
            };
        }

        public async Task<IEnumerable<courseToReturnDto>> GetAcceptedCoursesAsync()
        {
            #region MyRegion
            //var courseRepository = _unitOfWork.GetRepository<Course>();
            //var courses = await courseRepository.GetAllAsync(c => c.Teacher, c => c.CoursesTableSlots, c => c.CoursesReviews, c => c.StudentsCourses);

            //var coursesToReturn = courses.Where(c => c.Status == Status.Accepted)
            //  .Select(c => new courseToReturnDto
            //  {
            //      Name = c.Name,
            //      Price = c.Price,
            //      Location = c.Location,
            //      Num_Of_Students_Joined = c.Num_Of_Students_Joined,
            //      Photo = c.Photo,
            //      Description = c.Description,
            //      Capacity = c.Capacity,
            //      status = c.Status.ToString(),
            //      TeacherName = c.Teacher.Name,
            //      CourseReviews = c.CoursesReviews.Select(review => new ReviewsToReturnDto
            //      {
            //          Rating = review.Rating,
            //          Review = review.Review,
            //          courseName = c.Name,
            //          userName = review.User?.Name
            //      }).ToList(),

            //      EnrolledStudents = c.StudentsCourses.Select(sc => new EnrolledStudentsDto
            //      {
            //          StudentId = sc.StdId,
            //          StudentName = sc.User?.Name
            //      }).ToList()
            //    ,
            //      TableSlot = new TableSlotDto
            //      {
            //          StartDate = c.CoursesTableSlots.First().StartDate,
            //          EndDate = c.CoursesTableSlots.First().EndDate,
            //          StartHour = c.CoursesTableSlots.First().StartHour,
            //          EndHour = c.CoursesTableSlots.First().EndHour,
            //          DaysOff = c.CoursesTableSlots.First().DaysOff.ToList()

            //      }

            //  })
            //  .ToList();

            //return coursesToReturn; 
            #endregion
            
            IQueryBuilder<Course> courseQuery = new()
            {
                Criteria = (course => course.Status == Status.Accepted)
,
                Includes = [c => c.Teacher,
                            c => c.StudentsCourses, 
                            c => c.CoursesReviews , 
                            c=>c.CoursesTableSlot]

            };
            var courses = await _unitOfWork.GetRepository<Course>()
                                          .GetAllAsyncWithQueryBuilder(courseQuery);

            var coursesToReturn = courses.Select(c => new courseToReturnDto
            {
                Id = c.Id,
                Name = c.Name,
                Price = c.Price,
                Location = c.Location,
                Num_Of_Students_Joined = c.Num_Of_Students_Joined,
                Photo = c.Photo,
                Description = c.Description,
                Capacity = c.Capacity,
                status = c.Status.ToString(),
                TeacherName = c.Teacher.Name,
                //CourseReviews = c.CoursesReviews.Select(review => new ReviewsToReturnDto
                //{
                //    Rating = review.Rating,
                //    Review = review.Review,
                //    courseName = c.Name,
                //    userName = review.User?.Name
                //}).ToList(),

                //EnrolledStudents = c.StudentsCourses.Select(sc => new EnrolledStudentsDto
                //{
                //    StudentId = sc.StudentId,
                //    StudentName = sc.Student?.Name
                //}).ToList(),

                TableSlot = new TableSlotToReturn
                {
                    startHour = c.CoursesTableSlot.StartHour,
                    endHour = c.CoursesTableSlot.EndHour,
                    Dates = c.CoursesTableSlot.Dates,
                }
            })
            .ToList();

            return coursesToReturn;
        }
        

        public async Task CreateCourseAsync(courseToCreateDto courseDto)
        {
            byte[] photoBytes = null;
            if (courseDto.Photo != null)
            {
                photoBytes = await courseDto.Photo.ConvertToArrayOfBytes();
                if (photoBytes == null)
                {

                    return;
                }

                var course = new Course
                {
                    Name = courseDto.Name,
                    Photo = photoBytes,
                    Price = courseDto.Price,
                    Capacity = courseDto.Capacity,
                    Description = courseDto.Description,
                    Location = courseDto.Location,
                    TeacherId = courseDto.TeacherId //token
                };

                course.Status = Status.Pending;
                await _unitOfWork.GetRepository<Course>().CreateAsync(course);
                await _unitOfWork.CommitAsync();

                var tableSlot = courseDto.TableSlot;


                var courseTableSlots = new CourseTableSlot
                {
                    Dates = tableSlot.Dates.ToArray(),
                    StartHour = tableSlot.startHour,
                    EndHour = tableSlot.endHour,
                    CourseId = course.Id

                };

                await _unitOfWork.GetRepository<CourseTableSlot>().CreateAsync(courseTableSlots);
                await _unitOfWork.CommitAsync();

            }
        }

        //# bool?
        public async Task UpdateCourseAsync( courseToUpdateDto courseDto)
        {
            
            var course = await _unitOfWork.GetRepository<Course>()
                                        .GetAsync(courseDto.courseId, c => c.Teacher, c => c.CoursesTableSlot);

            if (course == null)
            {
                throw new ArgumentException("Course not found");
            }

            if (courseDto.Price.HasValue)
            {
                course.Price = courseDto.Price.Value;
            }
            if (courseDto.Capacity.HasValue)
            {
                course.Capacity = courseDto.Capacity.Value;
            }
            if (courseDto.Photo != null)
            {
                using (var stream = courseDto.Photo.OpenReadStream())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        course.Photo = memoryStream.ToArray();
                    }
                }
            }
            if (!string.IsNullOrEmpty(courseDto.Description))
            {
                course.Description = courseDto.Description;
            }
            if (!string.IsNullOrEmpty(courseDto.Location))
            {
                course.Location = courseDto.Location;
            }
            if (!string.IsNullOrEmpty(courseDto.Name))
            {
                course.Name = courseDto.Name;
            }

            if (courseDto.TableSlot != null)
            {
                var tableSlot = course.CoursesTableSlot;

                courseDto.TableSlot.SetUpdateDatesFlag();

                if (tableSlot != null)
                {
                    if (courseDto.TableSlot?.startHour != null)
                    {
                        tableSlot.StartHour = courseDto.TableSlot.startHour;
                    }

                    if (courseDto.TableSlot?.endHour != null)
                    {
                        tableSlot.EndHour = courseDto.TableSlot.endHour;
                    }

                    if (courseDto.TableSlot.UpdateDates)
                    {
                        tableSlot.Dates = courseDto.TableSlot.Dates;  // Update dates if flag is true
                    }

                    await _unitOfWork.GetRepository<CourseTableSlot>().UpdateAsync(tableSlot);
                }
            }

            await _unitOfWork.CommitAsync();
        }


        public async Task DeleteCourseAsync(int id)
        {
            var course = await _unitOfWork.GetRepository<Course>().GetAsync(id);

            if (course == null)
            {
                throw new ArgumentException("Course not found");
            }

            await _unitOfWork.GetRepository<Course>().DeleteAsync(course);
            await _unitOfWork.CommitAsync();
        }

        //****************************************************************************
        public async Task AddReviewAsync( ReviewsToCreateDto reviewDto)
        {
            var courseRepository = _unitOfWork.GetRepository<Course>();
           var course = await courseRepository.GetAsync(reviewDto.CourseId);

            var newReview = new CourseReview
            {
                UserId = reviewDto.UserId, // token
                CourseId = reviewDto.CourseId,
                Rating = reviewDto.Rating,
                Review = reviewDto.Review
            };
            await _unitOfWork.GetRepository<CourseReview>().CreateAsync(newReview);

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<ReviewsToReturnDto>> GetCourseReviewsAsync(int courseId)
        {

            //var queryBuilder = new IQueryBuilder<CourseReview>();
            //queryBuilder.Includes = new List<Expression<Func<CourseReview, object>>>()
            //{
            //    cr => cr.Course,
            //    cr => cr.User
            //};

            //var reviews = await _unitOfWork.GetRepository<CourseReview>()
            //                              .GetAllAsyncWithQueryBuilder(queryBuilder);


            IQueryBuilder<CourseReview> courseQuery = new()
            {
                Criteria = (course => course.Course.Id == courseId)
,
                Includes = [cr => cr.Course,
                            cr => cr.User ]

            };
            var reviews = await _unitOfWork.GetRepository<CourseReview>()
                                          .GetAllAsyncWithQueryBuilder(courseQuery);

            return reviews.Select(cr => new ReviewsToReturnDto
            {
                ReviewId = cr.Id,
                Rating = cr.Rating,
                Review = cr.Review,
                courseId = cr.Course.Id,
                courseName = cr.Course?.Name,
                userName = cr.User?.Name
            });
        }

        public async Task DeleteReviewAsync(ReviewToDeleteDto reviewToDeleteDto)
        {
            var courseRepository = _unitOfWork.GetRepository<Course>();
            var course = await courseRepository.GetAsync(reviewToDeleteDto.CourseId);



            var reviewRepository = _unitOfWork.GetRepository<CourseReview>();

            var review = await reviewRepository.GetAsync(reviewToDeleteDto.ReviewId);

            await reviewRepository.DeleteAsync(review);
            await _unitOfWork.CommitAsync();

        }

        //****************************************************************************

        public async Task<JoinCourseResult> JoinCourseAsync(coursesToJoinDto coursesToJoinDto)
        {
            var courseRepository = _unitOfWork.GetRepository<Course>();
            var studentCourseRepository = _unitOfWork.GetRepository<StudentCourse>();

            var studentCourseQueryBuilder = new IQueryBuilder<StudentCourse>()
            {
                Criteria = sc => sc.CourseId == coursesToJoinDto.CourseId && sc.StudentId == coursesToJoinDto .StudentId
            };

            var enrolledStudentCourses = await studentCourseRepository.GetAllAsyncWithQueryBuilder(studentCourseQueryBuilder);

            if (enrolledStudentCourses.Any())
            {
                return JoinCourseResult.AlreadyEnrolled;
            }

            var course = await courseRepository.GetAsync(coursesToJoinDto.CourseId);

            if (course == null)
            {
                return JoinCourseResult.CourseNotFound;
            }

            if (course.Num_Of_Students_Joined >= course.Capacity)
            {
                return JoinCourseResult.CourseFull;
            }

            var studentCourse = new StudentCourse
            {
                CourseId = coursesToJoinDto.CourseId,
                StudentId = coursesToJoinDto.StudentId
            };

           
           await  studentCourseRepository.CreateAsync(studentCourse);
            
            course = await courseRepository.GetAsync(coursesToJoinDto.CourseId);
            course.Num_Of_Students_Joined++;
            await courseRepository.UpdateAsync(course);
            await _unitOfWork.CommitAsync();
            return JoinCourseResult.Success;
        }

        public async Task<CancelCourseResults> CancelCourse(coursesToJoinDto coursesToJoinDto)
        {

            if (coursesToJoinDto.CourseId <= 0 || coursesToJoinDto.StudentId <= 0)
            {
                return CancelCourseResults.CourseOrStudentId;
            }
            var courseRepository = _unitOfWork.GetRepository<Course>();
            var studentCourseRepository = _unitOfWork.GetRepository<StudentCourse>();

            var studentCourseQueryBuilder = new IQueryBuilder<StudentCourse>()
            {
                Criteria = sc => sc.CourseId == coursesToJoinDto.CourseId && sc.StudentId == coursesToJoinDto.StudentId
            };
            var course = await courseRepository.GetAsync(coursesToJoinDto.CourseId);

            var enrolledStudentCourses = await studentCourseRepository.GetAllAsyncWithQueryBuilder(studentCourseQueryBuilder);
            var studentCourseToDelete = enrolledStudentCourses.FirstOrDefault();

            if (studentCourseToDelete == null)
            {
                return CancelCourseResults.NotEnrolled;
            }
            await studentCourseRepository.DeleteAsync(studentCourseToDelete);

            course = await courseRepository.GetAsync(coursesToJoinDto.CourseId);
            course.Num_Of_Students_Joined--;
            await courseRepository.UpdateAsync(course);

            await _unitOfWork.CommitAsync();
            return CancelCourseResults.Success;

        }

        //****************************************************************************

        public async Task<IEnumerable<courseToReturnDto>> GetCoursesAsync()
        {
            IQueryBuilder<Course> courseQuery = new()
            {


                Includes = [c => c.Teacher,
                            c => c.CoursesTableSlot ,
                            c => c.StudentsCourses,
                            c => c.CoursesReviews ,]
                           

            };
            var courses = await _unitOfWork.GetRepository<Course>()
                                          .GetAllAsyncWithQueryBuilder(courseQuery);



            return courses.Select(course => new courseToReturnDto
            {
                Id= course.Id,
                Name = course.Name,
                Price = course.Price,
                Location = course.Location,
                Num_Of_Students_Joined = course.Num_Of_Students_Joined,
                Photo = course.Photo,
                Description = course.Description,
                Capacity = course.Capacity,
                status = course.Status.ToString(),
                TeacherName = course.Teacher.Name,
                //CourseReviews = course.CoursesReviews.Select(review => new ReviewsToReturnDto
                //{
                //    Rating = review.Rating,
                //    Review = review.Review,
                //    courseName = course.Name,
                //    userName = review.User?.Name
                //}).ToList(),

                //EnrolledStudents = course.StudentsCourses.Select(sc => new EnrolledStudentsDto
                //{
                //    StudentId = sc.StudentId,
                //    StudentName = sc.Student?.Name
                //}).ToList(),
                
                TableSlot = new TableSlotToReturn
                {
                    startHour = course.CoursesTableSlot.StartHour,
                    endHour = course.CoursesTableSlot.EndHour,
                    Dates = course.CoursesTableSlot.Dates,
                }

            });
        }

        public async Task<IEnumerable<EnrolledStudentsDto>> GetEnrolledStudentsAsync(int courseId)
        {

             IQueryBuilder<StudentCourse> queryBuilder = new()
             {
                Criteria = (course => course.Course.Id == courseId)
,
                Includes = [cr => cr.Course,
                            cr => cr.Student ]

            };
            var reviews = await _unitOfWork.GetRepository<StudentCourse>()
                                          .GetAllAsyncWithQueryBuilder(queryBuilder);

            return reviews.Select(cr => new EnrolledStudentsDto
            {
               CourseId = cr.Course.Id,
               CourseName = cr.Course.Name,
                StudentId = cr.Student.Id,
                StudentName = cr.Student.Name
            });
        }


        public async Task<IEnumerable<EnrolledStudentsDto>> GetAllStudentsAsync()
        {

            var queryBuilder = new IQueryBuilder<StudentCourse>();
            queryBuilder.Includes = new List<Expression<Func<StudentCourse, object>>>()
            {
                cr => cr.Course,
                cr => cr.Student
            };

            var students = await _unitOfWork.GetRepository<StudentCourse>()
                                          .GetAllAsyncWithQueryBuilder(queryBuilder);

            return students.Select(cr => new EnrolledStudentsDto
            {
                CourseId = cr.Course.Id,
                CourseName = cr.Course.Name,
                StudentId = cr.Student.Id,
                StudentName = cr.Student.Name
            });
        }

        public async Task<IEnumerable<courseToReturnDto>> GetEmptyCoursesAsync()
        {
            IQueryBuilder<Course> courseQuery = new()
            {
                Criteria = course => course.Num_Of_Students_Joined == 0,

                Includes = [c => c.Teacher,
                 c => c.CoursesTableSlot ,
                 c => c.StudentsCourses,
                 c => c.CoursesReviews ,]


            };
            var courses = await _unitOfWork.GetRepository<Course>()
                                          .GetAllAsyncWithQueryBuilder(courseQuery);


            return courses.Select(course => new courseToReturnDto
            {
                Id = course.Id,
                Name = course.Name,
                Price = course.Price,
                Location = course.Location,
                Num_Of_Students_Joined = course.Num_Of_Students_Joined,
                Photo = course.Photo,
                Description = course.Description,
                Capacity = course.Capacity,
                status = course.Status.ToString(),
                TeacherName = course.Teacher.Name,
                
                TableSlot = new TableSlotToReturn
                {
                    startHour = course.CoursesTableSlot.StartHour,
                    endHour = course.CoursesTableSlot.EndHour,
                    Dates = course.CoursesTableSlot.Dates,
                }

            });




        }

        public async Task<IEnumerable<PendingCoursesDto>> GetPendingCoursesAsync()
        {
            var courseRepository = _unitOfWork.GetRepository<Course>();
            var courses = await courseRepository.GetAllAsync(c => c.Teacher, c => c.CoursesTableSlot);

            var coursesToReturn = courses.Where(c => c.Status == Status.Pending)
              .Select(c => new PendingCoursesDto
              {
                  Id = c.Id,
                  Name = c.Name,
                  Price = c.Price,
                  Location = c.Location,
                  Photo = c.Photo,
                  Description = c.Description,
                  Capacity = c.Capacity,
                  status = c.Status.ToString(),
                  TeacherName = c.Teacher.Name
                 
                ,
                  TableSlot = new TableSlotToReturn
                  {
                      startHour = c.CoursesTableSlot.StartHour,
                      endHour = c.CoursesTableSlot.EndHour,
                      Dates = c.CoursesTableSlot.Dates,
                  }

              })
              .ToList();

            return coursesToReturn;
        }


        public async Task<IEnumerable<ReviewsToReturnDto>> GetAllCoursesReviewsAsync()
        {

            var queryBuilder = new IQueryBuilder<CourseReview>();
            queryBuilder.Includes = new List<Expression<Func<CourseReview, object>>>()
            {
                cr => cr.Course,
                cr => cr.User
            };

            var reviews = await _unitOfWork.GetRepository<CourseReview>()
                                          .GetAllAsyncWithQueryBuilder(queryBuilder);

            return reviews.Select(cr => new ReviewsToReturnDto
            {
                ReviewId = cr.Id,
                Rating = cr.Rating,
                Review = cr.Review,
                courseId = cr.Course.Id,
                courseName = cr.Course?.Name,
                userName = cr.User?.Name
            });
        }

       public async Task<ManageCoursesResults> ApproveCourse(coursesToManageDto coursesToManageDto)
        {
            var course = await _unitOfWork.GetRepository<Course>().GetAsync(coursesToManageDto.CourseId);
            if (course == null)
            {
                return ManageCoursesResults.CourseNotFound;
            }

            if (course.Status != Status.Pending)
            {
                return ManageCoursesResults.AllreadyApproved;
            }

            course.Status = Status.Accepted;


            course.AdminId = coursesToManageDto.AdminId;  // token

            await _unitOfWork.CommitAsync();

            return ManageCoursesResults.Success;
        }


        public  async Task<ManageCoursesResults> RejectCourse(coursesToManageDto coursesToManageDto)
        {
            var course = await _unitOfWork.GetRepository<Course>().GetAsync(coursesToManageDto.CourseId);

            if (course == null)
            {
                return ManageCoursesResults.CourseNotFound;
            }

            if (course.Status != Status.Pending)
            {
                return ManageCoursesResults.CannotReject;
            }

            course.Status = Status.Refused;

            await _unitOfWork.GetRepository<Course>().DeleteAsync(course); // Use UnitOfWork's repository access

           
                await _unitOfWork.CommitAsync();
                return ManageCoursesResults.Success;
            

        }



    }

}



