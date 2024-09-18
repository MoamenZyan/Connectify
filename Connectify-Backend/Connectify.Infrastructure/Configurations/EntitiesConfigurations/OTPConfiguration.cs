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
    public class OTPConfiguration : IEntityTypeConfiguration<OTP>
    {
        public void Configure(EntityTypeBuilder<OTP> builder)
        {
            builder.ToTable("OTPs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                    .HasColumnType("UNIQUEIDENTIFIER")
                    .IsRequired();

            builder.Property(x => x.Number)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(10)
                    .IsRequired();

            builder.Property(x => x.Deadline)
                    .HasColumnType("DATETIME2")
                    .IsRequired();

            builder.Property(x => x.UserId)
                    .HasColumnType("UNIQUEIDENTIFIER")
                    .HasMaxLength(256)
                    .IsRequired();
        }
    }
}
