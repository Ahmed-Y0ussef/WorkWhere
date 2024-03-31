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
    internal class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {

        //    builder.HasOne(c => c.CoursesTableSlot)
        //.WithOne(s => s.Course)
        //.HasForeignKey<CoursesTableSlot>(s => s.CourseId) // Foreign key on Schedule
        //.IsRequired(true);



            builder.HasOne(c => c.Teacher)
               .WithMany(u => u.TaughtedCourses)
               .HasForeignKey(c => c.TeacherId)
              .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Admin)
            .WithMany(u => u.AcceptedCourses)
            .HasForeignKey(c => c.AdminId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Status)
            .HasDefaultValue(Status.Pending);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(8,2)");

            builder.Property(c => c.Status)
                .HasConversion<string>();
        }
    }
}
