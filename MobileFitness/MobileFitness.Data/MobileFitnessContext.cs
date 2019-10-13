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

        public DbSet<Food> Foods { get; set; }

        public DbSet<Macronutrient> Macronutrients { get; set; }

        public DbSet<Meal> Meals { get; set; }

        public DbSet<MealFood> MealFoods { get; set; }

        public DbSet<Weight> Weights { get; set; }

        public DbSet<UserMacronutrient> UsersMacronutrients { get; set; }

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

            modelBuilder.ApplyConfiguration(new FoodConfig());

            modelBuilder.ApplyConfiguration(new MacronutrientConfig());

            modelBuilder.ApplyConfiguration(new MealConfig());

            modelBuilder.ApplyConfiguration(new WeightConfg());

            modelBuilder.ApplyConfiguration(new MealFoodConfig());

            modelBuilder.ApplyConfiguration(new UserMacronutrientConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
