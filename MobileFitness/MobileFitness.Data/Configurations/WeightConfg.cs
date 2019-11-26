namespace MobileFitness.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    using MobileFitness.Models;


    /// <summary>
    /// Клас за настройка на таблицата за тегло
    /// </summary>
    public class WeightConfg : IEntityTypeConfiguration<Weight>
    {
        /// <summary>
        /// Настройва таблицата
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Weight> builder)
        {
            builder
                .ToTable("Weights");

            builder
                .HasKey(w => w.Id);

            builder
                .HasOne(w => w.User)
                .WithMany(u => u.Weights)
                .HasForeignKey(w => w.UserId);
        }
    }
}
