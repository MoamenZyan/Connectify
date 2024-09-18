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
    public class UserChatConfiguration : IEntityTypeConfiguration<UserChat>
    {
        public void Configure(EntityTypeBuilder<UserChat> builder)
        {
            builder.ToTable("UserChats");
            builder.HasKey(x => new { x.UserId, x.ChatId });

            builder.Property(x => x.Role)
                    .HasConversion(
                        v => v.ToString(),
                        v => (UserRole)Enum.Parse(typeof(UserRole), v))
                    .IsRequired();


            // Relations
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserJoinedChats)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Chat)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
