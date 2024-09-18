using Connectify.Domain.Entities;
using Connectify.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connectify.Infrastructure.Configurations.EntitiesConfigurations
{
    public class FriendRequestConfiguration : IEntityTypeConfiguration<FriendRequest>
    {
        public void Configure(EntityTypeBuilder<FriendRequest> builder)
        {
            builder.ToTable("FriendRequests");
            builder.HasKey(x => new { x.SenderId, x.ReceiverId });

            builder.Property(x => x.CreatedAt)
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(x => x.Status)
                    .HasConversion(
                        x => x.ToString(),
                        x => (FriendRequestStatus)Enum.Parse(typeof(FriendRequestStatus), x)
                    ).IsRequired();

            // Relations
            builder.HasOne(x => x.Sender)
                .WithMany(x => x.SentFriendRequests)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Receiver)
                .WithMany(x => x.ReceivedFriendRequests)
                .HasForeignKey(x => x.ReceiverId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
