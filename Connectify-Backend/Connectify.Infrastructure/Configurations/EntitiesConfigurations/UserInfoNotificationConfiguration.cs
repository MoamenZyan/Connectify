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
    public class UserInfoNotificationConfiguration : IEntityTypeConfiguration<UserInfoNotification>
    {
        public void Configure(EntityTypeBuilder<UserInfoNotification> builder)
        {
            builder.ToTable("UserInfoNotifications");
            builder.HasKey(x => new { x.UserId, x.NotificationId });

            // Relations
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserInfoNotifications)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Notification)
                    .WithMany(x => x.Users)
                    .HasForeignKey(x => x.NotificationId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
