
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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .HasColumnType("UNIQUEIDENTIFIER")
                    .IsRequired();

            builder.Property(x => x.Content)
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(4000)
                    .IsRequired();

            builder.Property(x => x.AttachmentPath)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(4000)
                    .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                    .HasColumnType("DATETIME2")
                    .IsRequired();

            builder.Property(x => x.Status)
                    .HasConversion(
                        v => v.ToString(),
                        v => (MessageStatus)Enum.Parse(typeof(MessageStatus), v)
                    ).IsRequired();

            // Relations
            builder.HasOne(x => x.Chat)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.ChatId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Sender)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.SenderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
