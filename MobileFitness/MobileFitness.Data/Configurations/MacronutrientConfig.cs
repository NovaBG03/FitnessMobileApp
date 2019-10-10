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
        }
    }
}
