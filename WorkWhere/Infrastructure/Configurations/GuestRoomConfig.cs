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
    internal class GuestRoomConfig : IEntityTypeConfiguration<GuestRoom>
    {
        public void Configure(EntityTypeBuilder<GuestRoom> builder)
        {
            builder.HasOne(gr => gr.Guest)
                           .WithMany(u => u.GuestRooms)
                           .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gr => gr.Room)
                .WithMany(r => r.GuestRooms)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasKey(gr => new { gr.GuestId, gr.RoomId });
        }
    }
}
