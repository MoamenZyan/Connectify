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
    public class UserPrivateChatConfiguration : IEntityTypeConfiguration<UserPrivateChat>
    {
        public void Configure(EntityTypeBuilder<UserPrivateChat> builder)
        {
            builder.ToTable("UserPrivateChats");
            builder.HasKey(x => new {x.User1Id, x.User2Id});

            // Relations
            builder.HasOne(x => x.User1)
                .WithMany(x => x.AsSender)
                .HasForeignKey(x => x.User1Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.User2)
                .WithMany(x => x.AsReceiver)
                .HasForeignKey(x => x.User2Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Chat)
                .WithMany(x => x.UserPrivateChats)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
