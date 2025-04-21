using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPort_API.Migrations
{
    /// <inheritdoc />
    public partial class zeby_b2a : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_Recipe_IdId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_Recipe_IdId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Recipe_IdId",
                table: "Ingredients",
                newName: "Recipe_Id");

            migrationBuilder.AddColumn<Guid>(
                name: "RecipeId",
                table: "Ingredients",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                table: "Ingredients",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_Recipes_RecipeId",
                table: "Ingredients");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "RecipeId",
                table: "Ingredients");

            migrationBuilder.RenameColumn(
                name: "Recipe_Id",
                table: "Ingredients",
                newName: "Recipe_IdId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Recipe_IdId",
                table: "Ingredients",
                column: "Recipe_IdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_Recipes_Recipe_IdId",
                table: "Ingredients",
                column: "Recipe_IdId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
