using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configurations
{
    internal class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasOne(sc => sc.Student)
                           .WithMany(u => u.StudentCourses)
                           .HasForeignKey(sc => sc.StudentId)
                           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Course)
               .WithMany(u => u.StudentsCourses)
               .HasForeignKey(sc => sc.CourseId)
               .OnDelete(DeleteBehavior.Cascade);


            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

        }
    }
}
