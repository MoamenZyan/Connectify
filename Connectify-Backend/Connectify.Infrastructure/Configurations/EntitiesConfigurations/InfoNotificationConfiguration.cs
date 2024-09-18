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
    public class InfoNotificationConfiguration : IEntityTypeConfiguration<InfoNotification>
    {
        public void Configure(EntityTypeBuilder<InfoNotification> builder)
        {
            builder.ToTable("InfoNotifications");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .HasColumnType("UNIQUEIDENTIFIER")
                    .IsRequired();

            builder.Property(x => x.Content)
                .HasColumnType("VARCHAR")
                .HasMaxLength(1024)
                .IsRequired();

            builder.Property(x => x.Type)
                    .HasConversion(
                        v => v.ToString(),
                        v => (NotificationType)Enum.Parse(typeof(NotificationType), v)
                    ).IsRequired();

            builder.Property(x => x.CreatedAt)
                    .HasColumnType("DATETIME2")
                    .IsRequired();
        }
    }
}
