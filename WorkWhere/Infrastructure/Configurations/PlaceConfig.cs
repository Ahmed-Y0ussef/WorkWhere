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
    internal class PlaceConfig : IEntityTypeConfiguration<Place>
    {
        public void Configure(EntityTypeBuilder<Place> builder)
        {
            builder.HasOne(p => p.Admin)
                           .WithMany(a => a.PlacesAccepted)
                           .HasForeignKey(p => p.AdminId)
                           .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Host)
                .WithMany(h => h.PlacesOwned)
                .HasForeignKey(p => p.HostID)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(c => c.Status)
               .HasConversion<string>()
               .HasDefaultValue(Status.Pending);

        }
    }
}
