﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using MobileFitness.Data;
using MobileFitness.Models.Enums;
using System;

namespace MobileFitness.Data.Migrations
{
    [DbContext(typeof(MobileFitnessContext))]
    partial class MobileFitnessContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MobileFitness.Models.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MacronutrientId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("MacronutrientId");

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("MobileFitness.Models.Macronutrient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<float>("Carbohydrate")
                        .HasColumnType("decimal(7,3)");

                    b.Property<float>("Fat")
                        .HasColumnType("decimal(7,3)");

                    b.Property<float>("Protein")
                        .HasColumnType("decimal(7,3)");

                    b.HasKey("Id");

                    b.ToTable("Macronutrients");
                });

            modelBuilder.Entity("MobileFitness.Models.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("MobileFitness.Models.MealFood", b =>
                {
                    b.Property<int>("MealId");

                    b.Property<int>("FoodId");

                    b.HasKey("MealId", "FoodId");

                    b.HasIndex("FoodId");

                    b.ToTable("MealsFoods");
                });

            modelBuilder.Entity("MobileFitness.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false);

                    b.Property<int>("Gender");

                    b.Property<int>("Goal");

                    b.Property<float>("HeightInMeters")
                        .HasColumnType("decimal(7,3)");

                    b.Property<int>("MacronutrientId");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("MacronutrientId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MobileFitness.Models.Weight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<float>("Kilograms")
                        .HasColumnType("decimal(7,3)");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Weights");
                });

            modelBuilder.Entity("MobileFitness.Models.Food", b =>
                {
                    b.HasOne("MobileFitness.Models.Macronutrient", "Macronutrient")
                        .WithMany("Foods")
                        .HasForeignKey("MacronutrientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MobileFitness.Models.Meal", b =>
                {
                    b.HasOne("MobileFitness.Models.User", "User")
                        .WithMany("Meals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MobileFitness.Models.MealFood", b =>
                {
                    b.HasOne("MobileFitness.Models.Food", "Food")
                        .WithMany("MealsFoods")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MobileFitness.Models.Meal", "Meal")
                        .WithMany("MealsFoods")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MobileFitness.Models.User", b =>
                {
                    b.HasOne("MobileFitness.Models.Macronutrient", "Macronutrient")
                        .WithMany("Users")
                        .HasForeignKey("MacronutrientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MobileFitness.Models.Weight", b =>
                {
                    b.HasOne("MobileFitness.Models.User", "User")
                        .WithMany("Weights")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
