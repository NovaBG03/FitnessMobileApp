using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MobileFitness.Data.Migrations
{
    /// <summary>
    /// Начална миграция
    /// </summary>
    public partial class InitialCreate : Migration
    {
        /// <summary>
        /// Прилагане на миграцията
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Macronutrients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Carbohydrate = table.Column<float>(type: "real", nullable: false),
                    Fat = table.Column<float>(type: "real", nullable: false),
                    Protein = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Macronutrients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Goal = table.Column<int>(type: "int", nullable: false),
                    HeightInMeters = table.Column<float>(type: "real", nullable: false),
                    Password = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Salt = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MacronutrientId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foods_Macronutrients_MacronutrientId",
                        column: x => x.MacronutrientId,
                        principalTable: "Macronutrients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersMacronutrients",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    MacronutrientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersMacronutrients", x => new { x.UserId, x.MacronutrientId });
                    table.ForeignKey(
                        name: "FK_UsersMacronutrients_Macronutrients_MacronutrientId",
                        column: x => x.MacronutrientId,
                        principalTable: "Macronutrients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersMacronutrients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kilograms = table.Column<float>(type: "real", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Weights_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealsFoods",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealsFoods", x => new { x.MealId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_MealsFoods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealsFoods_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Foods_MacronutrientId",
                table: "Foods",
                column: "MacronutrientId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_UserId",
                table: "Meals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MealsFoods_FoodId",
                table: "MealsFoods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersMacronutrients_MacronutrientId",
                table: "UsersMacronutrients",
                column: "MacronutrientId");

            migrationBuilder.CreateIndex(
                name: "IX_Weights_UserId",
                table: "Weights",
                column: "UserId");
        }

        /// <summary>
        /// Премахване на миграцията
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealsFoods");

            migrationBuilder.DropTable(
                name: "UsersMacronutrients");

            migrationBuilder.DropTable(
                name: "Weights");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Macronutrients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
