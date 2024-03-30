using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Contract.Course
{
    public interface ICourseReviewRepository
    {

        Task AddCourseReviewAsync(CourseReview courseReview);
        Task<CourseReview?> GetCourseReviewByIdAsync(int courseId, int id);
        Task<IEnumerable<CourseReview>> GetAllCourseReviewsAsync();
        Task DeleteCourseReviewAsync(int id);
    }
}
