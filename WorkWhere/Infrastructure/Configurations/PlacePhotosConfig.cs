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
    internal class PlacePhotosConfig : IEntityTypeConfiguration<PlacePhotos>
    {
        public void Configure(EntityTypeBuilder<PlacePhotos> builder)
        {
            builder.HasOne(p => p.Place)
                           .WithMany(p => p.PlacePhotos)
                           .HasForeignKey(r => r.PlaceId)
                           .OnDelete(DeleteBehavior.Cascade);

           // builder.HasKey(p => new { p.PlaceId, p.photo });
        }
    }
}
