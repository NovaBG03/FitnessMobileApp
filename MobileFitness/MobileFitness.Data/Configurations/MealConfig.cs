namespace MobileFitness.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    using MobileFitness.Models;

    /// <summary>
    /// Клас за настройка на таблицата за хранения
    /// </summary>
    public class MealConfig : IEntityTypeConfiguration<Meal>
    {
        /// <summary>
        /// Настройва таблицата
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Meal> builder)
        {
            builder
                .ToTable("Meals");

            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Name)
                .HasMaxLength(50)
                .IsRequired()
                .IsUnicode();

            builder
                .HasOne(m => m.User)
                .WithMany(u => u.Meals)
                .HasForeignKey(m => m.UserId);
        }
    }
}
