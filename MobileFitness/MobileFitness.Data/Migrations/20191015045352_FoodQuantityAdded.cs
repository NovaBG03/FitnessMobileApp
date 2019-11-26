using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MobileFitness.Data.Migrations
{
    /// <summary>
    /// Миграция добавяща количество на храната
    /// </summary>
    public partial class FoodQuantityAdded : Migration
    {
        /// <summary>
        /// Прилагане на миграцията
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "FoodQuantity",
                table: "MealsFoods",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <summary>
        /// Премахване на миграцията
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodQuantity",
                table: "MealsFoods");
        }
    }
}
