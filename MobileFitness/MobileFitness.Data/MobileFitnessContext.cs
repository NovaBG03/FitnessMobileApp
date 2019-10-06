using Microsoft.EntityFrameworkCore;
using System;

using MobileFitness.Models;
using MobileFitness.Data.Configurations;
using MobileFitness.Data;

using Xamarin.Forms;

[assembly: Dependency(typeof(MobileFitnessContext))]

namespace MobileFitness.Data
{
    public class MobileFitnessContext : DbContext
    {
        public MobileFitnessContext()
        {
        }

        public MobileFitnessContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=tcp:mobilefitness.database.windows.net,1433;Initial Catalog=MobileFitnessDb;Persist Security Info=False;User ID=nikita;Password=Koynov03;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
