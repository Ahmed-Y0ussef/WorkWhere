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
    internal class RoomReviewConfig : IEntityTypeConfiguration<RoomReview>
    {
        public void Configure(EntityTypeBuilder<RoomReview> builder)
        {
            builder.HasOne(r => r.Room)
               .WithMany(r => r.RoomReviews)
               .HasForeignKey(r => r.RoomId)
               .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(r => r.User)
                .WithMany(u=>u.roomReviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

           // builder.HasKey(r => new { r.RoomId, r.Id });
        }
    }
}
