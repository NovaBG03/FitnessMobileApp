namespace MobileFitness.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    using MobileFitness.Models;

    public class WeightConfg : IEntityTypeConfiguration<Weight>
    {
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
