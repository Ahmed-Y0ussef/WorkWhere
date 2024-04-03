using Application.DTO.Course;
using Application.DTO.CourseReviews;
using Application.Helpers.ResponseResults;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Contract
{
    public interface ICourseService
    {
        Task<IEnumerable<courseToReturnDto>> GetCoursesAsync();
        Task<courseToReturnDto> GetCourseAsync(int id);
        Task CreateCourseAsync(courseToCreateDto course);
        Task UpdateCourseAsync(/*int id,*/ courseToUpdateDto courseUpdateDto);
        Task DeleteCourseAsync(int id);
        //****************************************************************************
        Task AddReviewAsync(/*int courseId,*/ ReviewsToCreateDto reviewDto);
        public Task<IEnumerable<ReviewsToReturnDto>> GetCourseReviewsAsync(int courseId);

        Task DeleteReviewAsync(ReviewToDeleteDto reviewToDeleteDto);

        //****************************************************************************

        Task<JoinCourseResult> JoinCourseAsync(coursesToJoinDto coursesToManageDto);

        Task<CancelCourseResults> CancelCourse(coursesToJoinDto coursesToJoinDto);

        //****************************************************************************
    
        Task<IEnumerable<courseToReturnDto>> GetAcceptedCoursesAsync();
        Task<IEnumerable<PendingCoursesDto>> GetPendingCoursesAsync();

        public Task<IEnumerable<EnrolledStudentsDto>> GetEnrolledStudentsAsync(int courseId);

        public Task<IEnumerable<EnrolledStudentsDto>> GetAllStudentsAsync();

        public Task<IEnumerable<courseToReturnDto>> GetEmptyCoursesAsync();

       // public Task<IEnumerable<CourseStudentsDto>> GetAllStudentsAsync();


        public Task<IEnumerable<ReviewsToReturnDto>> GetAllCoursesReviewsAsync();


        public Task<ManageCoursesResults> ApproveCourse(coursesToManageDto coursesToManageDto);

        public  Task<ManageCoursesResults> RejectCourse(coursesToManageDto coursesToManageDto);

        //*******************************************************************

        public Task<IEnumerable<CoursesEnrolledInDto>> GetEnrolledCoursesAsync(int studentId);

        public Task<IEnumerable<courseToReturnDto>> GetTaughtCoursesAsync(int userId);






    }
}
