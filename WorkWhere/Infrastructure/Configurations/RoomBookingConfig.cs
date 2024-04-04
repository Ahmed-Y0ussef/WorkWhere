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
    internal class RoomBookingConfig : IEntityTypeConfiguration<RoomBooking>
    {
        public void Configure(EntityTypeBuilder<RoomBooking> builder)
        {
            builder.HasOne(gr => gr.Guest)
                           .WithMany(u => u.GuestRooms)
                           .HasForeignKey(gr => gr.GuestId)
                           .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(gr => gr.Room)
                .WithMany(r => r.Bookings)
                .HasForeignKey(r => r.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

           builder.HasOne(b=>b.place)
                .WithMany()
                .HasForeignKey(b=>b.PlaceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
