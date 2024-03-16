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
    internal class RoomConfig : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasOne(r => r.Admin)
                           .WithMany()
                           .HasForeignKey(r => r.AdminId)
                           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Place)
                .WithMany(p=>p.Rooms)
                .HasForeignKey(r => r.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(c => c.Status)
               .HasConversion<string>();
        }
    }
}
