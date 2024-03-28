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
    internal class RoomPhotosConfig : IEntityTypeConfiguration<RoomPhotos>
    {
        public void Configure(EntityTypeBuilder<RoomPhotos> builder)
        {
            builder.HasOne(r => r.Room)
                            .WithMany(r => r.RoomPhotos)
                            .HasForeignKey(r => r.RoomId)
                            .OnDelete(DeleteBehavior.Cascade);

           // builder.HasKey(r => new { r.photo, r.RoomId });
        }
    }
}
