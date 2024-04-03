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
    internal class CourseScheduleConfig : IEntityTypeConfiguration<CourseSchedule>
    {
        public void Configure(EntityTypeBuilder<CourseSchedule> builder)
        {
            //builder.HasOne(ct => ct.Course)
            //                .WithMany()
            //                .HasForeignKey(sc => sc.CourseId)
            //                .OnDelete(DeleteBehavior.Cascade);


            //builder.HasOne(ct => ct.Course)
            //               .WithMany()
            //               .HasForeignKey(sc => sc.CourseId)
            //               .OnDelete(DeleteBehavior.Cascade);

            // builder.HasKey(ct => new { ct.CourseId, ct.Dates , ct.StartHour});

            builder.HasKey(sc => new { sc.Id, sc.CourseId });


            // builder.Property(rt => rt.StartHour)
            //.HasColumnType("bigint");


            // builder.Property(rt => rt.EndHour)
            //.HasColumnType("bigint");
        }
    }
}
