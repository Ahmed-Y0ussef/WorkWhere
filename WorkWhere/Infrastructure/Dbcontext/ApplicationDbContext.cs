using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using System.Reflection;
namespace Infrastructure.Dbcontext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<CourseReview> courseReviews { get; set; }
        public DbSet<CourseTableSlot> courseTableSlots { get; set; }
        public DbSet<StudentCourse> studentCourses { get; set; }

       

        // Other DbSets for Room, Place, Course, Review, etc.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);

        }
    }
}
