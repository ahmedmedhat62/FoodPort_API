using Microsoft.AspNetCore.Mvc;
using FoodPort_API.Interfaces;
using FoodPort_API.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FoodPort_API.Models.DTOs;

namespace FoodPort_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly IRecipe _recipeService;
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _env;


        public RecipesController(IRecipe recipeService, IWebHostEnvironment env)
        {
            _recipeService = recipeService;
            _httpClient = new HttpClient(); // You can inject this too if you prefer DI
            _env = env;
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
        public async Task<IActionResult> CreateRecipe([FromForm] CreateRecipeDTO dto)
        {
            try
            {
                var result = await _recipeService.CreateRecipe(dto, _env);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
