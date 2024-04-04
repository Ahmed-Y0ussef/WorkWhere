using Application.DTO.Course;
using Core;
using Core.Application.Contract;
using Core.Entities;
using Core.Infrastructure.Contract;
using Application.DTO.CourseReviews;
using System.Linq.Expressions;
using Application.Helpers.ResponseResults;
using Application.DTO.CourseSchedule;


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
            var course = await _unitOfWork.GetRepo<Course>()
                                        .GetById(id, c => c.Teacher, c=>c.Admin,c => c.CourseSchedule);


            if (course == null)
            {
                
                return null; 
            }
             

            return new courseToReturnDto
            {
                Id= course.Id,
                AdminId = course.Admin?.Id ?? default(int),
                Name = course.Name,
                Price = course.Price,
                Location = course.Location,
                Num_Of_Students_Joined = course.NumOfStudentsEnrolled,
               // Nadoda = $"data:image/jpg;base64,{Convert.ToBase64String(course.Photo)}",
               Photo=course.Photo,
                Description = course.Description,
                Capacity = course.Capacity,
                status = course.Status.ToString(),
                TeacherName = course.Teacher.Name,
                CourseSchedule = new CourseScheduleToReturnDto
                { 
                    startHour = course.CourseSchedule?.StartHour,
                    endHour = course.CourseSchedule?.EndHour,
                    Dates = course.CourseSchedule?.Dates,
                } 
            };
        }

        public async Task<IEnumerable<courseToReturnDto>> GetAcceptedCoursesAsync()
        {
            
            IQueryBuilder<Course> courseQuery = new()
            {
                Criteria = (course => course.Status == Status.Accepted)
,
                Includes = [c => c.Teacher,
                            c=>c.Admin,
                            c => c.EnrolledStudents, 
                            c => c.CoursesReviews , 
                            c=>c.CourseSchedule]

            };
            var courses = await _unitOfWork.GetRepo<Course>()
                                          .GetAllAsyncWithQueryBuilder(courseQuery);

            var coursesToReturn = courses.Select(c => new courseToReturnDto
            {
                Id = c.Id,
                AdminId = c.Admin.Id,
                Name = c.Name,
                Price = c.Price,
                Location = c.Location,
                Num_Of_Students_Joined = c.NumOfStudentsEnrolled,
                Photo = c.Photo,
                Description = c.Description,
                Capacity = c.Capacity,
                status = c.Status.ToString(),
                TeacherName = c.Teacher.Name,
                CourseSchedule = new CourseScheduleToReturnDto
                { 
                    startHour = c.CourseSchedule.StartHour,
                    endHour = c.CourseSchedule.EndHour,
                    Dates = c.CourseSchedule.Dates,
                }
            })
            .ToList();

            return coursesToReturn;
        }

        public async Task CreateCourseAsync(courseToCreateDto courseDto)
        {
            
                var course = new Course
                {

                    Name = courseDto.Name,
                    Price = courseDto.Price,
                   // Photo = Convert.FromBase64String(courseDto.Photo),
                    NumOfStudentsEnrolled = courseDto?.Num_Of_Students_Joined ?? default(int),
                    Capacity = courseDto.Capacity,
                    Description = courseDto.Description,
                    Location = courseDto.Location,
                    TeacherId = courseDto.TeacherId //token
                    
                };
           // 1
            string coursePhoto = courseDto.Photo.Split(',')[1];
            course.Photo = Convert.FromBase64String(coursePhoto);


            course.Status = Status.Pending;
                await _unitOfWork.GetRepo<Course>().Add(course);
                await _unitOfWork.Complete();



            var CourseSchedules = new CourseSchedule

            {
                Dates = courseDto.Date,
                StartHour = courseDto.StartTime,
                EndHour = courseDto.EndTime,
                CourseId = course.Id


            };

            await _unitOfWork.GetRepo<CourseSchedule>().Add(CourseSchedules);
                await _unitOfWork.Complete();

            
        }

        //REFACTOOORRRRR
        //# bool?
        public async Task UpdateCourseAsync( courseToUpdateDto courseDto)
        {
            
            var course = await _unitOfWork.GetRepo<Course>()
                                        .GetById(courseDto.courseId, c => c.Teacher, c => c.CourseSchedule);

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
                string coursePhoto = courseDto.Photo.Split(',')[1];

                course.Photo = Convert.FromBase64String(coursePhoto);
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

            #region MyRegion
            //if (courseDto.CourseSchedule != null)
            //{
            //    var CourseSchedule = course.CourseSchedule;

            //    courseDto.CourseSchedule.SetUpdateDatesFlag();

            //    if (CourseSchedule != null)
            //    {
            //        if (courseDto.CourseSchedule?.startHour != null)
            //        {
            //            CourseSchedule.StartHour = courseDto.CourseSchedule.startHour;
            //        }

            //        if (courseDto.CourseSchedule?.endHour != null)
            //        {
            //            CourseSchedule.EndHour = courseDto.CourseSchedule.endHour;
            //        }

            //        if (courseDto.CourseSchedule.UpdateDates)
            //        {
            //            CourseSchedule.Dates = courseDto.CourseSchedule.Dates;  // Update dates if flag is true
            //        }

            //        await _unitOfWork.GetRepository<CourseSchedule>().UpdateAsync(CourseSchedule);
            //    }
            //} 
            #endregion

            if (courseDto.Dates != null || courseDto.startHour != null || courseDto.endHour != null)
            {
                if (course.CourseSchedule != null) // Check if CourseSchedule is not null first
                {
                    course.CourseSchedule.Dates = courseDto.Dates;
                    course.CourseSchedule.StartHour = courseDto.startHour;
                    course.CourseSchedule.EndHour = courseDto.endHour;
                }
            }


            await _unitOfWork.Complete();
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _unitOfWork.GetRepo<Course>().GetById(id);

            if (course == null)
            {
                throw new ArgumentException("Course not found");
            }

            await _unitOfWork.GetRepo<Course>().Delete(course);
            await _unitOfWork.Complete();
        }

        //********************************    Reviews     **********************************
        public async Task AddReviewAsync( ReviewsToCreateDto reviewDto)
        {
            var courseRepository = _unitOfWork.GetRepo<Course>();
           var course = await courseRepository.GetById(reviewDto.CourseId);

            var newReview = new CourseReviews
            {
                UserId = reviewDto.UserId, // token
                CourseId = reviewDto.CourseId,
                Rating = reviewDto.Rating,
                Review = reviewDto.Review
            };
            await _unitOfWork.GetRepo<CourseReviews>().Add(newReview);

            await _unitOfWork.Complete();
        }

        public async Task<IEnumerable<ReviewsToReturnDto>> GetCourseReviewsAsync(int courseId)
        {

            IQueryBuilder<CourseReviews> courseQuery = new()
            {
                Criteria = (course => course.Course.Id == courseId)
,
                Includes = [cr => cr.Course,
                            cr => cr.User ]

            };
            var reviews = await _unitOfWork.GetRepo<CourseReviews>()
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
            var courseRepository = _unitOfWork.GetRepo<Course>();
            var course = await courseRepository.GetById(reviewToDeleteDto.CourseId);



            var reviewRepository = _unitOfWork.GetRepo<CourseReviews>();

            var review = await reviewRepository.GetById(reviewToDeleteDto.ReviewId);

            await reviewRepository.Delete(review);
            await _unitOfWork.Complete();

        }

        //**********************************    Student     *******************************

        public async Task<JoinCourseResult> JoinCourseAsync(coursesToJoinDto coursesToJoinDto)
        {
            var courseRepository = _unitOfWork.GetRepo<Course>();
            var studentCourseRepository = _unitOfWork.GetRepo<EnrolledStudents>();

            var studentCourseQueryBuilder = new IQueryBuilder<EnrolledStudents>()
            {
                Criteria = sc => sc.CourseId == coursesToJoinDto.CourseId && sc.StudentId == coursesToJoinDto .StudentId
            };

            var enrolledStudentCourses = await studentCourseRepository.GetAllAsyncWithQueryBuilder(studentCourseQueryBuilder);

            if (enrolledStudentCourses.Any())
            {
                return JoinCourseResult.AlreadyEnrolled;
            }

            var course = await courseRepository.GetById(coursesToJoinDto.CourseId);

            if (course == null)
            {
                return JoinCourseResult.CourseNotFound;
            }

            if (course.NumOfStudentsEnrolled >= course.Capacity)
            {
                return JoinCourseResult.CourseFull;
            }

            var studentCourse = new EnrolledStudents
            {
                CourseId = coursesToJoinDto.CourseId,
                StudentId = coursesToJoinDto.StudentId
            };

           
           await  studentCourseRepository.Add(studentCourse);
            
            course = await courseRepository.GetById(coursesToJoinDto.CourseId);
            course.NumOfStudentsEnrolled++;
            await courseRepository.Update(course);
            await _unitOfWork.Complete();
            return JoinCourseResult.Success;
        }

        public async Task<CancelCourseResults> CancelCourse(coursesToJoinDto coursesToJoinDto)
        {

            if (coursesToJoinDto.CourseId <= 0 || coursesToJoinDto.StudentId <= 0)
            {
                return CancelCourseResults.CourseOrStudentId;
            }
            var courseRepository = _unitOfWork.GetRepo<Course>();
            var studentCourseRepository = _unitOfWork.GetRepo<EnrolledStudents>();

            var studentCourseQueryBuilder = new IQueryBuilder<EnrolledStudents>()
            {
                Criteria = sc => sc.CourseId == coursesToJoinDto.CourseId && sc.StudentId == coursesToJoinDto.StudentId
            };
            var course = await courseRepository.GetById(coursesToJoinDto.CourseId);

            var enrolledStudentCourses = await studentCourseRepository.GetAllAsyncWithQueryBuilder(studentCourseQueryBuilder);
            var studentCourseToDelete = enrolledStudentCourses.FirstOrDefault();

            if (studentCourseToDelete == null)
            {
                return CancelCourseResults.NotEnrolled;
            }
            await studentCourseRepository.Delete(studentCourseToDelete);

            course = await courseRepository.GetById(coursesToJoinDto.CourseId);
            course.NumOfStudentsEnrolled--;
            await courseRepository.Update(course);

            await _unitOfWork.Complete();
            return CancelCourseResults.Success;

        }

        //***********************************    Admin     **************************************

        public async Task<IEnumerable<courseToReturnDto>> GetCoursesAsync()
        {
            IQueryBuilder<Course> courseQuery = new()
            {


                Includes = [c => c.Teacher,
                            c=>c.Admin,
                            c => c.CourseSchedule ,
                            c => c.EnrolledStudents,
                            c => c.CoursesReviews ,]
                           

            };
            var courses = await _unitOfWork.GetRepo<Course>()
                                          .GetAllAsyncWithQueryBuilder(courseQuery);



            return courses.Select(course => new courseToReturnDto
            {
                Id= course.Id,
                AdminId = course.Admin?.Id ?? default(int),
                Name = course.Name,
                Price = course.Price,
                Location = course.Location,
                Num_Of_Students_Joined = course.NumOfStudentsEnrolled,
                Photo = course.Photo,
                Description = course.Description,
                Capacity = course.Capacity,
                status = course.Status.ToString(),
                TeacherName = course.Teacher.Name,
                CourseSchedule = new CourseScheduleToReturnDto
                {
                    startHour = course.CourseSchedule.StartHour,
                    endHour = course.CourseSchedule.EndHour,
                    Dates = course.CourseSchedule.Dates,
                }

            });
        }

        public async Task<IEnumerable<EnrolledStudentsDto>> GetEnrolledStudentsAsync(int courseId)
        {

             IQueryBuilder<EnrolledStudents> queryBuilder = new()
             {
                Criteria = (course => course.Course.Id == courseId)
,
                Includes = [cr => cr.Course,
                            cr => cr.Student ]

            };
            var students = await _unitOfWork.GetRepo<EnrolledStudents>()
                                          .GetAllAsyncWithQueryBuilder(queryBuilder);


            return students.GroupBy(cr => cr.Course.Id)
                 .Select(group => new EnrolledStudentsDto 
                 {
                     CourseId = group.Key,
                     Students = group.Select(cr => new StudentDto 
                     {
                         CourseName = cr.Course.Name,
                         StudentId = cr.Student.Id,
                         StudentName = cr.Student.Name
                     }).ToList()
                 });


        }


        public async Task<IEnumerable<EnrolledStudentsDto>> GetAllStudentsAsync()
        {

            var queryBuilder = new IQueryBuilder<EnrolledStudents>();
            queryBuilder.Includes = new List<Expression<Func<EnrolledStudents, object>>>()
            {
                cr => cr.Course,
                cr => cr.Student
            };

            var students = await _unitOfWork.GetRepo<EnrolledStudents>()
                                          .GetAllAsyncWithQueryBuilder(queryBuilder);

            return students.GroupBy(cr => cr.Course.Id)
                 .Select(group => new EnrolledStudentsDto 
                 {
                     CourseId = group.Key,
                     Students = group.Select(cr => new StudentDto 
                     {
                         CourseName = cr.Course.Name,
                         StudentId = cr.Student.Id,
                         StudentName = cr.Student.Name
                     }).ToList()
                 });
        }

        public async Task<IEnumerable<courseToReturnDto>> GetEmptyCoursesAsync()
        {
            IQueryBuilder<Course> courseQuery = new()
            {
                Criteria = course => course.NumOfStudentsEnrolled == 0,

                Includes = [c => c.Teacher,
                          c=>c.Admin,
                         c => c.CourseSchedule ,
                         c => c.EnrolledStudents,
                         c => c.CoursesReviews ,]


            };
            var courses = await _unitOfWork.GetRepo<Course>()
                                          .GetAllAsyncWithQueryBuilder(courseQuery);


            return courses.Select(course => new courseToReturnDto
            {
                Id = course.Id,
                AdminId = course.Admin?.Id ?? default(int),
                Name = course.Name,
                Price = course.Price,
                Location = course.Location,
                Num_Of_Students_Joined = course.NumOfStudentsEnrolled,
                Photo = course.Photo,
                Description = course.Description,
                Capacity = course.Capacity,
                status = course.Status.ToString(),
                TeacherName = course.Teacher.Name,

                CourseSchedule = new CourseScheduleToReturnDto
                {
                    startHour = course.CourseSchedule.StartHour,
                    endHour = course.CourseSchedule.EndHour,
                    Dates = course.CourseSchedule.Dates,
                }

            });
        }

        public async Task<IEnumerable<PendingCoursesDto>> GetPendingCoursesAsync()
        {
            var courseRepository = _unitOfWork.GetRepo<Course>();
            var courses = await courseRepository.GetAllAsync(c => c.Teacher, c => c.CourseSchedule);

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
                  CourseSchedule = new CourseScheduleToReturnDto
                  {
                      startHour = c.CourseSchedule.StartHour,
                      endHour = c.CourseSchedule.EndHour,
                      Dates = c.CourseSchedule.Dates,
                  }

              })
              .ToList();

            return coursesToReturn;
        }


        public async Task<IEnumerable<ReviewsToReturnDto>> GetAllCoursesReviewsAsync()
        {

            var queryBuilder = new IQueryBuilder<CourseReviews>();
            queryBuilder.Includes = new List<Expression<Func<CourseReviews, object>>>()
            {
                cr => cr.Course,
                cr => cr.User
            };

            var reviews = await _unitOfWork.GetRepo<CourseReviews>()
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
            var course = await _unitOfWork.GetRepo<Course>().GetById(coursesToManageDto.CourseId);
            if (course == null)
            {
                return ManageCoursesResults.CourseNotFound;
            }

            if (course.Status != Status.Pending)
            {
                return ManageCoursesResults.AllreadyApproved;
            }

            course.Status = Status.Accepted;


           course.AdminId= coursesToManageDto.AdminId ;  // token

            await _unitOfWork.Complete();

            return ManageCoursesResults.Success;
        }


        public  async Task<ManageCoursesResults> RejectCourse(coursesToManageDto coursesToManageDto)
        {
            var course = await _unitOfWork.GetRepo<Course>().GetById(coursesToManageDto.CourseId);

            if (course == null)
            {
                return ManageCoursesResults.CourseNotFound;
            }

            if (course.Status != Status.Pending)
            {
                return ManageCoursesResults.CannotReject;
            }

            course.Status = Status.Refused;

            await _unitOfWork.GetRepo<Course>().Delete(course); // Use UnitOfWork's repository access

           
                await _unitOfWork.Complete();
                return ManageCoursesResults.Success;
            

        }


        //***********************************    User     *****************************************
       
        // courses enrolled in
        public async Task<IEnumerable<CoursesEnrolledInDto>> GetEnrolledCoursesAsync(int studentId)
        {
            IQueryBuilder<EnrolledStudents> queryBuilder = new()
            {
                Criteria = (enrollment => enrollment.Student.Id == studentId),
                Includes = [cr => cr.Course, cr => cr.Student , c=>c.Course.CourseSchedule ,c=>c.Course.Admin, c=>c.Student.EnrolledStudents]  // Include CourseSchedule for efficient data retrieval
            };

            var enrollments = await _unitOfWork.GetRepo<EnrolledStudents>()
                                              .GetAllAsyncWithQueryBuilder(queryBuilder);

            return enrollments.GroupBy(cr => cr.Student.Id)
                    .Select(group => new CoursesEnrolledInDto
                    {
                        StudentId = group.Key,
                        EnrolledCourses = group.Select(cr => new courseToReturnDto
                        {
                            Id = cr.Course.Id,
                            AdminId = cr.Course.Admin?.Id ?? default(int),
                            Name = cr.Course.Name,
                            Price = cr.Course.Price,
                            Location = cr.Course.Location,
                            Description = cr.Course.Description,
                            Num_Of_Students_Joined = cr.Course.NumOfStudentsEnrolled,
                            Photo = cr.Course.Photo,
                            Capacity = cr.Course.Capacity,
                            status = cr.Course.Status.ToString(),
                            TeacherName = cr.Course.Teacher.Name,
                            CourseSchedule = new CourseScheduleToReturnDto
                            {
                                startHour = cr.Course.CourseSchedule.StartHour,
                                endHour = cr.Course.CourseSchedule.EndHour,
                                Dates = cr.Course.CourseSchedule.Dates
                            }

                        }).ToList()
                    });
        }

        // get courses i added

        public async Task<IEnumerable<courseToReturnDto>> GetTaughtCoursesAsync(int userId)
        {
            IQueryBuilder<Course> courseQuery = new()
            {
                Criteria = (course => course.TeacherId == userId)
,
                Includes = [c => c.Teacher,
                            c=>c.Admin,
                            c => c.EnrolledStudents,
                            c => c.CoursesReviews ,
                            c=>c.CourseSchedule]

            };
            var courses = await _unitOfWork.GetRepo<Course>()
                                          .GetAllAsyncWithQueryBuilder(courseQuery);

            var coursesToReturn = courses.Select(c => new courseToReturnDto
            {
                Id = c.Id,
                AdminId = c.Admin?.Id ?? default(int),
                Name = c.Name,
                Price = c.Price,
                Location = c.Location,
                Num_Of_Students_Joined = c.NumOfStudentsEnrolled,
                Photo = c.Photo,
                Description = c.Description,
                Capacity = c.Capacity,
                status = c.Status.ToString(),
                TeacherName = c.Teacher.Name,
                CourseSchedule = new CourseScheduleToReturnDto
                {
                    startHour = c.CourseSchedule.StartHour,
                    endHour = c.CourseSchedule.EndHour,
                    Dates = c.CourseSchedule.Dates,
                }
            })
            .ToList();

            return coursesToReturn;

        }







    }

}



