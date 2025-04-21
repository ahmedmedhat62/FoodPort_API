using System;
using System.Collections.Generic;
using FoodPort_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FoodPort_API.Data
{
    public class Seed
    {
        public static void Seeding(IApplicationBuilder applicationBuilder)
        {
            /*using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();

                context.Database.EnsureCreated();

                // Check if the database already has any recipes
               
                
                    // Create a new recipe
                    var recipe = new Recipe
                    {
                        Id = Guid.NewGuid(),
                        Title = "Classic Spaghetti Carbo",
                        Description = "A classic Italian pasta dish with eggs, cheese, pancetta, and pepper.",
                        Image = "https://example.com/spaghetti-carbonara.jpg",
                        AuthorId = Guid.NewGuid(),
                        CreatedAt = DateTime.UtcNow,
                        PrepTime = 20,
                        Servings = 4,
                        Difficulty = Difficulty.Easy,
                        
                    };

                    recipe.Likes = new List<Like>
                    {
                         new Like
                         {
                            Id = Guid.NewGuid(),
                            RecipeId = recipe.Id,
                            UserId = Guid.NewGuid(), // Replace with real user GUIDs
                            LikedAt = DateTime.UtcNow
                         },
                        new Like
                        {
                            Id = Guid.NewGuid(),
                            RecipeId = recipe.Id,
                            UserId = Guid.NewGuid(),
                            LikedAt = DateTime.UtcNow.AddMinutes(-15)
                        }
                    };
                    recipe.Saves = new List<Saves>
                    {
                        new Saves
                        {
                            Id = Guid.NewGuid(),
                            RecipeId = recipe.Id,
                            UserId = Guid.NewGuid(), // Replace with real user GUIDs
                            SavedAt = DateTime.UtcNow
                        },
                        new Saves
                        {
                            Id = Guid.NewGuid(),
                            RecipeId = recipe.Id,
                            UserId = Guid.NewGuid(),
                            SavedAt = DateTime.UtcNow.AddMinutes(-30)
                        }
                    };

                    recipe.Comments = new List<Comment>
                    {
                        new Comment
                        {
                            Id = Guid.NewGuid(),
                            AuthorId = Guid.NewGuid(),
                            RecipeId = recipe.Id,
                            Content = "This recipe is amazing!",
                            PostedAt = DateTime.UtcNow
                        }
                    };

                    recipe.Tags = new List<Tag>
                    {
                        new Tag { Id = Guid.NewGuid(), Name = "Italian" },
                        new Tag { Id = Guid.NewGuid(), Name = "Pasta" }

                    };
                    // Add ingredients
                    recipe.Ingredients = new List<Ingredient>
                    {
                        new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id, // Now recipe.Id is accessible
                            Name = "Spaghetti",
                            Quantity = 400,
                            Unit = MeasurementUnit.Grams,
                            WeightInGrams = 400
                        },
                        new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            Name = "Pancetta",
                            Quantity = 150,
                            Unit = MeasurementUnit.Grams,
                            WeightInGrams = 150
                        },
                        new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            Name = "Eggs",
                            Quantity = 2,
                            Unit = MeasurementUnit.Amount,
                            WeightInGrams = 100
                        },
                        new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            Name = "Parmesan Cheese",
                            Quantity = 50,
                            Unit = MeasurementUnit.Grams,
                            WeightInGrams = 50
                        },
                        new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            Name = "Black Pepper",
                            Quantity = 1,
                            Unit = MeasurementUnit.Amount,
                            WeightInGrams = 5
                        }
                    };

                    // Add instructions
                    recipe.Instructions = new List<Instruction>
                    {
                        new Instruction
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            StepNumber = 1,
                            Description = "Cook the spaghetti in a large pot of boiling salted water until al dente."
                        },
                        new Instruction
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            StepNumber = 2,
                            Description = "While the pasta is cooking, fry the pancetta in a large skillet until crispy."
                        },
                        new Instruction
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            StepNumber = 3,
                            Description = "In a bowl, whisk together the eggs and grated Parmesan cheese."
                        },
                        new Instruction
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            StepNumber = 4,
                            Description = "Drain the pasta and add it to the skillet with the pancetta. Remove from heat."
                        },
                        new Instruction
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            StepNumber = 5,
                            Description = "Quickly stir in the egg and cheese mixture, allowing the residual heat to cook the eggs. Add black pepper to taste."
                        }
                    };

                    // Add nutrition facts
                    recipe.Nutrition = new NutritionFacts
                    {
                        Id = Guid.NewGuid(),
                        Recipe_Id = recipe.Id,
                        Calories = 600,
                        Fat = 25,
                        Carbohydrates = 65,
                        Protein = 30,
                        Sugar = 5
                    };

                    // Add the recipe to the context
                    context.Recipes.Add(recipe);

                    // Save changes to the database
                    context.SaveChanges();
                
            }*/
        }
    }
}