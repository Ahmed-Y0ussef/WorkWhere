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
    internal class RoomUtilitiesConfig : IEntityTypeConfiguration<RoomUtilities>
    {
        public void Configure(EntityTypeBuilder<RoomUtilities> builder)
        {
            builder.HasOne(u => u.Room)
                            .WithMany(p => p.RoomUtilities)
                            .HasForeignKey(r => r.RoomId)
                            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
