namespace MobileFitness.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    using MobileFitness.Models;

    public class MealFoodConfig : IEntityTypeConfiguration<MealFood>
    {
        public void Configure(EntityTypeBuilder<MealFood> builder)
        {
            builder
                .ToTable("MealsFoods");

            builder
                .HasKey(mf => new { mf.MealId, mf.FoodId });

            builder
                .HasOne(mf => mf.Meal)
                .WithMany(m => m.MealsFoods)
                .HasForeignKey(mf => mf.MealId);

            builder
                .HasOne(mf => mf.Food)
                .WithMany(f => f.MealsFoods)
                .HasForeignKey(mf => mf.FoodId);
        }
    }
}
