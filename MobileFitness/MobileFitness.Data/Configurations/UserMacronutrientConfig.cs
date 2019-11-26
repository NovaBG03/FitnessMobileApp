namespace MobileFitness.Data.Configurations
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using MobileFitness.Models;


    /// <summary>
    /// Клас за настройка на двойно-свързана таблица за потребители и макронутриенти
    /// </summary>
    public class UserMacronutrientConfig : IEntityTypeConfiguration<UserMacronutrient>
    {
        /// <summary>
        /// Настройва таблицата
        /// </summary>
        /// <param name="builder"></param>
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
