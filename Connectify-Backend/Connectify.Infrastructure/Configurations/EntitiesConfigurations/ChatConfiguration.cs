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
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("Chats");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .HasColumnType("UNIQUEIDENTIFIER")
                    .IsRequired();

            builder.Property(x => x.Name)
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(256)
                    .IsRequired();

            builder.Property(x => x.Description)
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(4000)
                    .IsRequired();

            builder.Property(x => x.CreatedAt)
                    .HasColumnType("DATETIME2")
                    .IsRequired();

            builder.Property(x => x.Photo)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(4000)
                    .IsRequired();

            builder.Property(x => x.Type)
                .HasConversion(
                x => x.ToString(),
                x => (ChatType)Enum.Parse(typeof(ChatType), x)
            ).IsRequired();
        }
    }
}
