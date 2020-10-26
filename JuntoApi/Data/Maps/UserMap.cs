using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using JuntoApi.Models;

namespace JuntoApi.Data.Maps
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

            builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");

            builder.Property(x => x.Role)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnType("varchar(50)");
        }
    }
}