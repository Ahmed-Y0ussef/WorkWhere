using Core.Entities;
using Core.Infrastructure.Contract.Course;
using Infrastructure.Dbcontext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Course
{
    public class CourseReviewRepository : ICourseReviewRepository
    {
        private readonly ApplicationDbContext dbcontext;

        public CourseReviewRepository(ApplicationDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task AddCourseReviewAsync(CourseReview courseReview)
        {
            //await Task.Run(() => courseReviews.Add(courseReview));

            await dbcontext.Set<CourseReview>().AddAsync(courseReview);
            await dbcontext.SaveChangesAsync();
        }

        public async Task DeleteCourseReviewAsync(int id)
        {
            var entity = await dbcontext.Set<CourseReview>().FindAsync(id);
            if (entity == null)
            {

                throw new ArgumentException("Course review with the given ID was not found.", nameof(id));
            }

            dbcontext.Set<CourseReview>().Remove(entity);
            await dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<CourseReview>> GetAllCourseReviewsAsync()
        {
            return await dbcontext.Set<CourseReview>().ToListAsync();
        }

        //public async Task<CourseReview?> GetCourseReviewByIdAsync(int id)
        //{
        //    return await dbcontext.Set<CourseReview>().FindAsync(id);
        //}
        public async Task<CourseReview?> GetCourseReviewByIdAsync(int courseId, int id)
        {
            return await dbcontext.Set<CourseReview>()
                .FirstOrDefaultAsync(cr => cr.CourseId == courseId && cr.Id == id);
        }
    }
}
