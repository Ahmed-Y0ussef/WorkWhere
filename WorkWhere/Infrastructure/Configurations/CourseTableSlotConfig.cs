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
    internal class CourseTableSlotConfig : IEntityTypeConfiguration<CourseTableSlot>
    {
        public void Configure(EntityTypeBuilder<CourseTableSlot> builder)
        {
            builder.HasOne(ct => ct.Course)
                            .WithMany(c=>c.CoursesTableSlots)
                            .HasForeignKey(sc => sc.CourseId)
                            .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(ct => new { ct.CourseId, ct.Date });

            builder.Property(rt => rt.StratHour)
           .HasColumnType("bigint");


            builder.Property(rt => rt.EndHour)
           .HasColumnType("bigint");
        }
    }
}
