using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FoodPort_API.Data
{
    public class Seed
    {
        public static void Seeding(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
               /* var context = serviceScope.ServiceProvider.GetService<DataContext>();
                var logger = serviceScope.ServiceProvider.GetService<ILogger<Seed>>();

                if (context == null)
                {
                    throw new Exception("DataContext not found in service provider.");
                }

                context.Database.EnsureCreated();

                // Log start of deletion
                logger?.LogInformation("Starting removal of related entities...");

                // Remove Tags related to recipes
                var allTags = context.Tags.ToList();
                if (allTags.Any())
                {
                    context.Tags.RemoveRange(allTags);
                    logger?.LogInformation("Removed all tags.");
                }

                // Remove other related entities
                var allIngredients = context.Ingredients.ToList();
                if (allIngredients.Any())
                {
                    context.Ingredients.RemoveRange(allIngredients);
                    logger?.LogInformation("Removed all ingredients.");
                }

                var allInstructions = context.Instructions.ToList();
                if (allInstructions.Any())
                {
                    context.Instructions.RemoveRange(allInstructions);
                    logger?.LogInformation("Removed all instructions.");
                }

                var allLikes = context.Likes.ToList();
                if (allLikes.Any())
                {
                    context.Likes.RemoveRange(allLikes);
                    logger?.LogInformation("Removed all likes.");
                }

                var allSaves = context.Saves.ToList();
                if (allSaves.Any())
                {
                    context.Saves.RemoveRange(allSaves);
                    logger?.LogInformation("Removed all saves.");
                }

                var allComments = context.Comments.ToList();
                if (allComments.Any())
                {
                    context.Comments.RemoveRange(allComments);
                    logger?.LogInformation("Removed all comments.");
                }

                var allNutrition = context.NutritionFacts.ToList();
                if (allNutrition.Any())
                {
                    context.NutritionFacts.RemoveRange(allNutrition);
                    logger?.LogInformation("Removed all nutrition facts.");
                }

                // Now you can safely delete all recipes
                var allRecipes = context.Recipes.ToList();
                if (allRecipes.Any())
                {
                    context.Recipes.RemoveRange(allRecipes);
                    logger?.LogInformation("Removed all recipes.");
                }

                // Save all changes to the database
                context.SaveChanges();
                logger?.LogInformation("Database seeding and cleanup completed.");*/
            }
        }
    }
}