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
    public class UserFriendConfiguration : IEntityTypeConfiguration<UserFriend>
    {
        public void Configure(EntityTypeBuilder<UserFriend> builder)
        {
            builder.ToTable("UserFriends");
            builder.HasKey(x => new { x.UserId1, x.UserId2 });

            // Relations
            builder.HasOne(x => x.User1)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId1)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User2)
                .WithMany(x => x.FriendOf)
                .HasForeignKey(x => x.UserId2)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
