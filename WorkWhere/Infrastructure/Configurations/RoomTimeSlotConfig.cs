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
    internal class RoomTimeSlotConfig : IEntityTypeConfiguration<RoomTimeSlot>
    {
        public void Configure(EntityTypeBuilder<RoomTimeSlot> builder)
        {
            builder.HasOne(rt => rt.Room)
                            .WithMany(r=>r.RoomTimeSlots)
                            .HasForeignKey(rt => rt.RoomId)
                            .OnDelete(DeleteBehavior.Restrict);

            builder.Property(rt => rt.TimeStrart)
            .HasColumnType("bigint");

            builder.Property(rt => rt.TimeEnd)
           .HasColumnType("bigint");

            builder.HasKey(rt => new { rt.RoomId, rt.Date, rt.TimeStrart });
        }
    }
}
