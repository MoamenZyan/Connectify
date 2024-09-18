using Connectify.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Configurations.EntitiesConfigurations
{
    public class UserBlocksConfiguration : IEntityTypeConfiguration<UserBlocks>
    {
        public void Configure(EntityTypeBuilder<UserBlocks> builder)
        {
            builder.ToTable("UserBlocks");
            builder.HasKey(x => new { x.BlockerId, x.BlockedId });

            // Relations
            builder.HasOne(x => x.BlockerUser)
                .WithMany(x => x.BlockedUsers)
                .HasForeignKey(x => x.BlockerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.BlockedUser)
                .WithMany(x => x.BlockedFrom)
                .HasForeignKey(x => x.BlockedId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
