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
    internal class PlaceUtilitiesConfig : IEntityTypeConfiguration<PlaceUtilities>
    {
        public void Configure(EntityTypeBuilder<PlaceUtilities> builder)
        {
            builder.HasOne(u => u.Place)
               .WithMany(p => p.PlaceUtilities)
               .HasForeignKey(u => u.PlaceId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
