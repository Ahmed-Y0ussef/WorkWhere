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
    internal class PlaceReviewConfig : IEntityTypeConfiguration<PlaceReview>
    {
        public void Configure(EntityTypeBuilder<PlaceReview> builder)
        {
            builder.HasOne(p => p.Place)
                .WithMany(p => p.PlaceReviews)
                .HasForeignKey(p => p.PlaceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.User)
                .WithMany(u=>u.placeReviews)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
