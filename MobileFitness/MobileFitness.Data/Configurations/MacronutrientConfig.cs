namespace MobileFitness.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    using MobileFitness.Models;

    public class MacronutrientConfig : IEntityTypeConfiguration<Macronutrient>
    {
        public void Configure(EntityTypeBuilder<Macronutrient> builder)
        {
            builder
                .ToTable("Macronutrients");

            builder
                .HasKey(m => m.Id);

            builder
                .Property(m => m.Protein)
                .HasColumnType("decimal(7,3)");

            builder
                .Property(m => m.Fat)
                .HasColumnType("decimal(7,3)");

            builder
                .Property(m => m.Carbohydrate)
                .HasColumnType("decimal(7,3)");
        }
    }
}
