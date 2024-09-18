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
    public class UserSeenMessageConfiguration : IEntityTypeConfiguration<UserSeenMessage>
    {
        public void Configure(EntityTypeBuilder<UserSeenMessage> builder)
        {
            builder.ToTable("UserSeenMessages");
            builder.HasKey(x => new { x.UserId, x.MessageId });

            builder.Property(x => x.SeenAt)
                    .HasColumnType("DATETIME2")
                    .IsRequired();


            // Relations
            builder.HasOne(x => x.User)
                .WithMany(x => x.SeenMessages)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Message)
                .WithMany(x => x.MessageViewers)
                .HasForeignKey(x => x.MessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
