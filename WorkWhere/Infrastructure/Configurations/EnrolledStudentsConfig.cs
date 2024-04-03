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
    internal class EnrolledStudentsConfig : IEntityTypeConfiguration<EnrolledStudents>
    {
        public void Configure(EntityTypeBuilder<EnrolledStudents> builder)
        {
            builder.HasOne(sc => sc.Student)
                           .WithMany(u => u.EnrolledStudents)
                           .HasForeignKey(sc => sc.StudentId)
                           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(sc => sc.Course)
               .WithMany(u => u.EnrolledStudents)
               .HasForeignKey(sc => sc.CourseId)
               .OnDelete(DeleteBehavior.Cascade);


            builder.HasKey(sc => new { sc.StudentId, sc.CourseId });

        }
    }
}
