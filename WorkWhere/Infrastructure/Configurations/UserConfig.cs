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
    internal class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
           builder.HasOne(c => c.Admin)
                           .WithMany()
                           .HasForeignKey(u => u.AdminID)
                           .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(c => c.Status)
               .HasConversion<string>();
        }
    }
}
