namespace MobileFitness.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    using MobileFitness.Models;

    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users");

            builder
                .HasKey(u => u.Id);

            builder
                .Property(u => u.Username)
                .HasMaxLength(50)
                .IsUnicode()
                .IsRequired();

            builder
                .Property(u => u.Password)
                .HasMaxLength(250)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(u => u.Salt)
                .HasMaxLength(250)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(u => u.Email)
                .HasMaxLength(100)
                .IsRequired()
                .IsUnicode(false);

            builder
                .HasOne(u => u.Macronutrient)
                .WithMany(m => m.Users)
                .HasForeignKey(u => u.MacronutrientId);

            builder
                .Property(u => u.HeightInMeters)
                .HasColumnType("decimal(7,3)");
        }
    }
}
