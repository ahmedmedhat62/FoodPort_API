using FoodPort_API.Data;
using FoodPort_API.Interfaces;
using FoodPort_API.Migrations;
using FoodPort_API.Models;
using FoodPort_API.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace FoodPort_API.Interfaces
{
    public interface IRecipe
    {
        // Get all recipes
        IEnumerable<Recipe> GetAllRecipes();
        Task<Recipe> CreateRecipe(CreateRecipeDTO Recipe);
        Task<NutritionFacts?> GetNutritionFactsAsync(string ingredientQuery, Guid recipe_id);
        Task<NutritionFacts?> GetNutritionFactsByRecipeIdAsync(Guid recipe_id);

        // Get a recipe by its ID
        Recipe GetRecipeById(Guid id);
    }
}
namespace FoodPort_API.Services
{
    public class RecipeService : IRecipe
    {
        private readonly DataContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        // Inject the DataContext via constructor
        public RecipeService(DataContext context, HttpClient httpClient, IConfiguration configuration)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        // Get all recipes
        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _context.Recipes
                .Include(r => r.Ingredients) // Include ingredients
                .Include(r => r.Instructions) // Include instructions
                .Include(r => r.Nutrition) // Include nutrition facts
                .Include(r => r.Saves)
                .Include(r => r.Comments)
                .Include(r => r.Likes)
                .Include(r => r.Tags)
                .ToList();
        }

        // Get a recipe by its ID with related entities
        public Recipe GetRecipeById(Guid id)
        {
            return _context.Recipes
                .Include(r => r.Ingredients) // Include ingredients
                .Include(r => r.Instructions) // Include instructions
                .Include(r => r.Nutrition) // Include nutrition facts
                .Include(r => r.Tags)
                .Include(r => r.Saves)
                .Include(r => r.Comments)
                .Include(r => r.Likes)
                .FirstOrDefault(r => r.Id == id);
        }
        public async Task<NutritionFacts?> GetNutritionFactsAsync(string ingredientQuery , Guid recipe_id)
        {
            var apiKey = _configuration["xAY03EFjWAF7OrWeKTx8yw==Aj2sLvSqnw53BDEi"]; // Store your API key in appsettings.json or secrets
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://api.api-ninjas.com/v1/nutrition?query={Uri.EscapeDataString(ingredientQuery)}");

            request.Headers.Add("X-Api-Key", "xAY03EFjWAF7OrWeKTx8yw==Aj2sLvSqnw53BDEi");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            var factsArray = JsonSerializer.Deserialize<JsonElement>(json);

            if (factsArray.ValueKind != JsonValueKind.Array)
                return null;

            var result = new NutritionFacts();

            foreach (var item in factsArray.EnumerateArray())
            {
               
                result.fat_total += item.GetProperty("fat_total_g").GetDouble();
                result.Fat_saturated += item.GetProperty("fat_saturated_g").GetDouble();
                result.Carbohydrates += item.GetProperty("carbohydrates_total_g").GetDouble();
                result.fiber += item.GetProperty("fiber_g").GetDouble();
                result.Sugar += item.GetProperty("sugar_g").GetDouble();
            }
            result.Recipe_Id = recipe_id;

            return result;
        }

        /*public async Task<string> CreateRecipe( CreateRecipeDTO dto, IWebHostEnvironment env)
        {
            try
            {
                // Step 1: Handle image upload
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(dto.Image.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                    throw new ArgumentException("Unsupported file format");

                var uploadsFolder = Path.Combine(env.WebRootPath, "uploads", "recipes");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await dto.Image.CopyToAsync(stream);
                }

                // Step 2: Map CreateRecipeDTO to Recipe domain model
                var recipe = new Recipe
                {
                    Id = Guid.NewGuid(),
                    Title = dto.Title,
                    Description = dto.Description,
                    Image = $"/uploads/recipes/{fileName}", // relative path
                    CreatedAt = DateTime.UtcNow,
                    PrepTime = dto.PrepTime,
                    Servings = dto.Servings,
                    Difficulty = dto.Difficulty,
                    AuthorId = dto.AuthorId // AuthorId passed from frontend, assuming the user is authenticated
                };
                var ingredientsList = new List<Ingredient>();
                foreach (var ingredientDto in dto.Ingredients)
                {
                    var ingredient = new Ingredient
                    {
                        Id = Guid.NewGuid(),
                        Recipe_Id = recipe.Id,  // Link this ingredient to the created recipe
                        Name = ingredientDto.Name,
                        Quantity = ingredientDto.Quantity,
                        Unit = (Models.MeasurementUnit)ingredientDto.Unit,
                        WeightInGrams = 400 // Calculate weight
                    };
                    ingredientsList.Add(ingredient);
                    _context.Ingredients.Add(ingredient);

                }

                recipe.Ingredients = ingredientsList;

                // Step 3: Map IngredientDTO to Ingredient domain model
                /*recipe.Ingredients = dto.Ingredients.Select(i => new Ingredient
                {
                    Id = Guid.NewGuid(),
                    Recipe_Id = recipe.Id,  // Link this ingredient to the created recipe
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Unit = (Models.MeasurementUnit)i.Unit,
                    WeightInGrams = i.Quantity * 1 // Optional: Adjust conversion logic for weight if needed
                }).ToList();

                // Step 4: Map InstructionDTO to Instruction domain model
                recipe.Instructions = dto.Instructions.Select(i => new Instruction
                {
                    Id = Guid.NewGuid(),
                    Recipe_Id = recipe.Id,  // Link this instruction to the created recipe
                    StepNumber = i.StepNumber,
                    Description = i.Description
                }).ToList();

                // Step 5: Map TagDTO to Tag domain model
                recipe.Tags = dto.Tags.Select(t => new Tag
                {
                    Id = Guid.NewGuid(),
                    Name = t.Name
                }).ToList();

                var ingredient1 = new Ingredient 
                {
                    Id = Guid.NewGuid(),
                    Name = "zobru",
                    Quantity = 300,
                    Unit = MeasurementUnit.Grams , 
                    Recipe_Id =recipe.Id,
                    WeightInGrams = 400

                };

                // Step 6: Add the recipe to the context and save to the database
                _context.Ingredients.Add(ingredient1); 
                _context.Recipes.Add(recipe);

                // Add ingredients
                

                // Add instructions
                foreach (var instruction in recipe.Instructions)
                {
                    _context.Instructions.Add(instruction);
                }

                // Add tags
                foreach (var tag in recipe.Tags)
                {
                    _context.Tags.Add(tag);
                }

                await _context.SaveChangesAsync();

               

                // Step 7: Once the recipe is saved, update the Recipe_Id for Ingredients and Instructions
                

                foreach (var instruction in recipe.Instructions)
                {
                    instruction.Recipe_Id = recipe.Id;
                }

                // Save the changes again to ensure all entities are linked properly
                await _context.SaveChangesAsync();

                return "Recipe created successfully";
            }
            catch (Exception ex)
            {
                // Handle exceptions gracefully
                return $"Error creating recipe: {ex.Message}";
            }
        }*/
        public async Task<NutritionFacts?> GetNutritionFactsByRecipeIdAsync(Guid recipeId)
        {
            return await _context.NutritionFacts
                .FirstOrDefaultAsync(n => n.Recipe_Id == recipeId);
        }
        public async Task<Recipe> CreateRecipe(CreateRecipeDTO dto)
        {
            try
            {
                // Step 1: Create Recipe object with placeholder image
                var recipeId = Guid.NewGuid();
                var recipe = new Recipe
                {
                    Id = recipeId,
                    Title = dto.Title,
                    Description = dto.Description,
                    Image = "/uploads/recipes/placeholder.png", // Placeholder image
                    CreatedAt = DateTime.UtcNow,
                    PrepTime = dto.PrepTime,
                    Servings = dto.Servings,
                    Difficulty = dto.Difficulty,
                    AuthorId = dto.AuthorId,
                    Ingredients = new List<Ingredient>(),
                    Instructions = new List<Instruction>(),
                    Tags = new List<Tag>()
                };
                var ingredientQueryBuilder = new StringBuilder();
                // Step 2: Map Ingredients
                if (dto.Ingredients != null)
                {
                    foreach (var i in dto.Ingredients)
                    {
                        recipe.Ingredients.Add(new Ingredient
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            Name = i.Name,
                            Quantity = i.Quantity,
                            Unit = i.Unit,
                            WeightInGrams = i.Quantity * 1 // You can adjust logic here
                        });
                        ingredientQueryBuilder.Append($"{i.Quantity} {i.Unit} {i.Name} ");
                    }
                }
                var ingredientQuery = ingredientQueryBuilder.ToString().Trim();

                Console.WriteLine( ingredientQuery );
                // Step 3: Map Instructions
                if (dto.Instructions != null)
                {
                    foreach (var instr in dto.Instructions)
                    {
                        recipe.Instructions.Add(new Instruction
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            StepNumber = instr.StepNumber,
                            Description = instr.Description
                        });
                    }
                }

                // Step 4: Map Tags
                if (dto.Tags != null)
                {
                    foreach (var t in dto.Tags)
                    {
                        recipe.Tags.Add(new Tag
                        {
                            Id = Guid.NewGuid(),
                            Recipe_Id = recipe.Id,
                            Name = t.Name
                        });
                    }
                }

                // Step 5: Save to DB
                _context.Recipes.Add(recipe);
                await _context.SaveChangesAsync();
                var nutrition = await GetNutritionFactsAsync(ingredientQuery, recipe.Id);
                if (nutrition != null)
                {
                    _context.NutritionFacts.Add(nutrition);
                    await _context.SaveChangesAsync();
                    recipe.Nutrition = nutrition;
                }

                return recipe;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating recipe: {ex.Message}");
            }
        }



    }
}
