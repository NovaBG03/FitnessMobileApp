using Microsoft.EntityFrameworkCore;
using System;

using MobileFitness.Models;
using MobileFitness.Data.Configurations;
using MobileFitness.Data;

using Xamarin.Forms;

[assembly: Dependency(typeof(MobileFitnessContext))]
/// <summary>
/// Връзка с базата дани
/// </summary>
namespace MobileFitness.Data
{
    /// <summary>
    /// Клас за връзка с MobileFitness база данни
    /// </summary>
    public class MobileFitnessContext : DbContext
    {
        /// <summary>
        /// Създава нова връзка с базата данни, използвайки настройки по подразбиране
        /// </summary>
        public MobileFitnessContext()
        {
        }

        /// <summary>
        /// Създава нова връзка с базата данни, използвайки конкретни настройки
        /// </summary>
        /// <param name="options">Настройки</param>
        public MobileFitnessContext(DbContextOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Таблица с потребители
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Таблица с храни
        /// </summary>
        public DbSet<Food> Foods { get; set; }

        /// <summary>
        /// Таблица с макронутриенти
        /// </summary>
        public DbSet<Macronutrient> Macronutrients { get; set; }

        /// <summary>
        /// Таблица с хранения
        /// </summary>
        public DbSet<Meal> Meals { get; set; }

        /// <summary>
        /// Двойно-свързана таблица на хранения и храни 
        /// </summary>
        public DbSet<MealFood> MealFoods { get; set; }

        /// <summary>
        /// Таблица с тегло
        /// </summary>
        public DbSet<Weight> Weights { get; set; }

        /// <summary>
        /// Двойно-свързана таблица на потребители и макронутриенти  
        /// </summary>
        public DbSet<UserMacronutrient> UsersMacronutrients { get; set; }

        /// <summary>
        /// Задава настойките на връзката
        /// </summary>
        /// <param name="optionsBuilder">Настройки за връзка</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer("Server=tcp:mobilefitness.database.windows.net,1433;Initial Catalog=MobileFitnessDb;Persist Security Info=False;User ID=nikita;Password=Koynov03;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Задава как да бъдат създадени таблиците в базата
        /// </summary>
        /// <param name="modelBuilder">Настройки за таблиците</param>
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
