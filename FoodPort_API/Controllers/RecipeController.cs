using Microsoft.AspNetCore.Mvc;
using FoodPort_API.Interfaces;
using FoodPort_API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FoodPort_API.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using FoodPort_API.Data;

namespace FoodPort_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipe _recipeService;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _env;
        private readonly DataContext _context;


        public RecipesController(IRecipe recipeService, IWebHostEnvironment env , DataContext context)
        {
            _recipeService = recipeService;
            _httpClient = new HttpClient(); // You can inject this too if you prefer DI
            _env = env;
            _context = context;
        }

        // GET: api/recipes
        [HttpGet]
        public ActionResult<IEnumerable<Recipe>> GetAllRecipes()
        {
            var recipes = _recipeService.GetAllRecipes();
            return Ok(recipes);
        }

        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public ActionResult<Recipe> GetRecipeById(Guid id)
        {
            var recipe = _recipeService.GetRecipeById(id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        // GET: api/recipes/autocomplete?query=pepper&limit=5
        [HttpGet("autocomplete")]
        public async Task<IActionResult> AutocompleteIngredient([FromQuery] string query, [FromQuery] int limit = 5)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query parameter is required.");

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://edamam-food-and-grocery-database.p.rapidapi.com/auto-complete?q={Uri.EscapeDataString(query)}&limit={limit}"),
            };

            request.Headers.Add("x-rapidapi-key", "6af8650c32msh3938f70626d3a36p15621cjsn0e38e70b5e5a");
            request.Headers.Add("x-rapidapi-host", "edamam-food-and-grocery-database.p.rapidapi.com");

            try
            {
                using var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return Ok(body); // or deserialize if you want to return a typed object
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, $"Error calling Edamam API: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeDTO dto)
        {
            try
            {
                var result = await _recipeService.CreateRecipe(dto );
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        /* [HttpGet("facts")]
         public async Task<IActionResult> GetNutritionFacts([FromQuery] string query)
         {
             if (string.IsNullOrWhiteSpace(query))
                 return BadRequest("Query cannot be empty.");

             var facts = await _recipeService.GetNutritionFactsAsync(query);
             if (facts == null)
                 return NotFound("No nutritional data found.");

             return Ok(facts);
         }*/
        [HttpGet("{id}/nutrition")]
        public async Task<IActionResult> GetNutritionFactsForRecipe(Guid id)
        {
            var nutrition = await _recipeService.GetNutritionFactsByRecipeIdAsync(id);
            if (nutrition == null)
            {
                return NotFound("Nutrition facts not found for this recipe.");
            }

            return Ok(nutrition);
        }
        [HttpPost("upload-image/{recipeId}")]
        public async Task<IActionResult> UploadRecipeImage(Guid recipeId, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(image.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                return BadRequest("Unsupported file format.");
            }

            var recipe = _context.Recipes.FirstOrDefault(r => r.Id == recipeId);
            if (recipe == null)
            {
                return NotFound("Recipe not found.");
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "recipes");
            Directory.CreateDirectory(uploadsFolder);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            recipe.Image = $"/uploads/recipes/{fileName}";
            await _context.SaveChangesAsync();

            return Ok(new { imageUrl = recipe.Image });
        }
    }
}
