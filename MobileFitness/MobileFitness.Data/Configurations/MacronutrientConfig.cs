﻿namespace MobileFitness.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    using MobileFitness.Models;

    /// <summary>
    /// Клас за настройка на таблицата за макронутриенти
    /// </summary>
    public class MacronutrientConfig : IEntityTypeConfiguration<Macronutrient>
    {
        /// <summary>
        /// Настройва таблицата
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Macronutrient> builder)
        {
            builder
                .ToTable("Macronutrients");

            builder
                .HasKey(m => m.Id);
        }
    }
}
