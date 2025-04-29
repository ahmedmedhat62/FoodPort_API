using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPort_API.Migrations
{
    /// <inheritdoc />
    public partial class nutrition_facts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "Fat",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "Protein",
                table: "NutritionFacts");

            migrationBuilder.AlterColumn<double>(
                name: "Sugar",
                table: "NutritionFacts",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<double>(
                name: "Carbohydrates",
                table: "NutritionFacts",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<double>(
                name: "Fat_saturated",
                table: "NutritionFacts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fat_total",
                table: "NutritionFacts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "fiber",
                table: "NutritionFacts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fat_saturated",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "fat_total",
                table: "NutritionFacts");

            migrationBuilder.DropColumn(
                name: "fiber",
                table: "NutritionFacts");

            migrationBuilder.AlterColumn<int>(
                name: "Sugar",
                table: "NutritionFacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "Carbohydrates",
                table: "NutritionFacts",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "NutritionFacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Fat",
                table: "NutritionFacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Protein",
                table: "NutritionFacts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
