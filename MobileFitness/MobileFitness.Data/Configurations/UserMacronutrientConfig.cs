namespace MobileFitness.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MobileFitness.Models;

    public class UserMacronutrientConfig : IEntityTypeConfiguration<UserMacronutrient>
    {
        public void Configure(EntityTypeBuilder<UserMacronutrient> builder)
        {
            builder
                .ToTable("UsersMacronutrients");

            builder
                .HasKey(um => new { um.UserId, um.MacronutrientId });

            builder
                .HasOne(um => um.User)
                .WithMany(u => u.UsersMacronutrients)
                .HasForeignKey(um => um.UserId);

            builder
                .HasOne(um => um.Macronutrient)
                .WithMany(m => m.UsersMacronutrients)
                .HasForeignKey(um => um.MacronutrientId);
        }
    }
}
