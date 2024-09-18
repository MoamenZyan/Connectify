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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnType("UNIQUEIDENTIFIER")
                .IsRequired();

            builder.Property(x => x.Fname)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(256)
                    .IsRequired();

            builder.Property(x => x.Lname)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(256)
                    .IsRequired();

            builder.Property(x => x.Email)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(256)
                    .IsRequired();

            builder.Property(x => x.Password)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(528)
                    .IsRequired();

            builder.Property(x => x.Phone)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(528)
                    .IsRequired();

            builder.Property(x => x.Photo)
                    .HasColumnType("VARCHAR")
                    .HasMaxLength(528)
                    .IsRequired();

            builder.Property(x => x.IsVerified)
                    .HasColumnType("BIT")
                    .IsRequired();

            builder.Property(x => x.IsOnline)
                    .HasColumnType("BIT")
                    .IsRequired();
        }
    }
}
