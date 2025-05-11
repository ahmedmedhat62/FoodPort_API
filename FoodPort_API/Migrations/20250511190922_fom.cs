using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodPort_API.Migrations
{
    /// <inheritdoc />
    public partial class fom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_ApplicationUserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ApplicationUserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "ApplicationUserRecipe",
                columns: table => new
                {
                    ApplicationUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SavedRecipesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRecipe", x => new { x.ApplicationUserId, x.SavedRecipesId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRecipe_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRecipe_Recipes_SavedRecipesId",
                        column: x => x.SavedRecipesId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRecipe_SavedRecipesId",
                table: "ApplicationUserRecipe",
                column: "SavedRecipesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserRecipe");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Recipes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ApplicationUserId",
                table: "Recipes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_ApplicationUserId",
                table: "Recipes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
