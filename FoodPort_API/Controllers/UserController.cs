using Microsoft.AspNetCore.Mvc;
using FoodPort_API.Interfaces;
using FoodPort_API.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;
using FoodPort_API.Data;
using FoodPort_API.Models.DTOs;
using FoodPort_API.Services;

namespace FoodPort_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly   IUser _userService;
        private readonly DataContext _context;

        public UsersController(IUser userService, DataContext context)
        {
            _userService = userService;
            _context = context;
        }

        // POST: api/users/register
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(dto);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        // POST: api/users/upload-profile-picture/{userId}
        [HttpPost("upload-profile-picture/{userId}")]
        public async Task<IActionResult> UploadProfilePicture(Guid userId, IFormFile image)
        {
            try
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

                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "profiles");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                user.ProfilePicture = $"/uploads/profiles/{fileName}";
                await _context.SaveChangesAsync();

                return Ok(new { imageUrl = user.ProfilePicture });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            try
            {
                var token = await _userService.LoginAsync(dto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // POST: api/users/{userId}/saved-recipes/{recipeId}
        [HttpPost("save")]
        public async Task<IActionResult> SaveRecipe(Guid userId, [FromBody] Guid recipeId)
        {
            try
            {
                await _userService.AddSavedRecipeAsync(userId, recipeId);
                return Ok("Recipe saved successfully.");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("saved/{userId}")]
        public async Task<IActionResult> GetSavedRecipes(Guid userId)
        {
            var recipes = await _userService.GetSavedRecipesAsync(userId);
            return Ok(recipes);
        }




    }
}