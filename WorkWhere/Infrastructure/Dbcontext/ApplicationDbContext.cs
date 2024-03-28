using Microsoft.EntityFrameworkCore;
using Core.Entities;
using System.Reflection;
namespace Infrastructure.Dbcontext
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
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
       // public DbSet<Place> Place { get; set; }
    }
}
