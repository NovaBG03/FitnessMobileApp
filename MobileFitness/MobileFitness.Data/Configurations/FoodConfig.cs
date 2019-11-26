/// <summary>
/// Настройки на таблиците
/// </summary>
namespace MobileFitness.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    using MobileFitness.Models;

    /// <summary>
    /// Клас за настройка на таблицата за храни
    /// </summary>
    public class FoodConfig : IEntityTypeConfiguration<Food>
    {
        /// <summary>
        /// Настройва таблицата
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder
                .ToTable("Foods");

            builder
                .HasKey(f => f.Id);

            builder
                .Property(f => f.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder
                .HasOne(f => f.Macronutrient)
                .WithMany(m => m.Foods)
                .HasForeignKey(f => f.MacronutrientId);
        }
    }
}
