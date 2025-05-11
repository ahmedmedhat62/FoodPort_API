using FoodPort_API.Models.DTOs;
using FoodPort_API.Models;
using FoodPort_API.Data;
using Microsoft.AspNetCore.Identity;
using FoodPort_API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoodPort_API.Auth;
using Microsoft.Extensions.Options;

namespace FoodPort_API.Services
{
    public interface IUser
    {
        Task<Guid> RegisterUserAsync(RegisterUserDTO registerDto);
        Task<ApplicationUser> GetUserByIdAsync(Guid userId);
        Task UpdateUserProfileAsync(Guid userId, UpdateUserProfileDTO updateDto);
        Task UploadProfilePictureAsync(Guid userId, IFormFile image);
        Task<string> LoginAsync(LoginDTO loginDto);
        //Task<List<Recipe>> AddRecipeToSavedAsync(Guid userId, Guid recipeId); // New method
        //Task<List<Recipe>> GetSavedRecipesAsync(Guid userId);
        Task AddSavedRecipeAsync(Guid userId, Guid recipeId);
        Task<ICollection<Recipe>> GetSavedRecipesAsync(Guid userId);
    }

    public class UserService : IUser
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DataContext _context;
        private readonly JWT_Token_Settings _jwtTokenSettings;
        public UserService(UserManager<ApplicationUser> userManager, DataContext context, IOptions<JWT_Token_Settings> jwt_token_options)
        {
            _userManager = userManager;
            _context = context;
            _jwtTokenSettings = jwt_token_options.Value;
        }
        public async Task<string> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new Exception("Invalid username or password.");
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtTokenSettings.SecretKey);

            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.UserName),
                  
                   new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString() )

                }),
                Expires = DateTime.UtcNow.AddSeconds(_jwtTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = _jwtTokenSettings.Audience,
                Issuer = _jwtTokenSettings.Issuer,
            };

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Guid> RegisterUserAsync(RegisterUserDTO registerDto)
        {
            var user = new ApplicationUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
               
               
               
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

          

            return user;
        }
       

        public async Task UpdateUserProfileAsync(Guid userId, UpdateUserProfileDTO updateDto)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

            
            user.Email = updateDto.Email;
            user.ProfilePicture = updateDto.ProfilePictureUrl;

            await _userManager.UpdateAsync(user);
        }
       

        public async Task UploadProfilePictureAsync(Guid userId, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                throw new Exception("No file uploaded.");
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(image.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                throw new Exception("Unsupported file format.");
            }

            var user = await GetUserByIdAsync(userId);
            if (user == null) throw new Exception("User not found.");

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
        }
        public async Task AddSavedRecipeAsync(Guid userId, Guid recipeId)
        {
            // Validate that the recipe exists
            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe == null)
            {
                throw new ArgumentException("Recipe not found.");
            }

            // Retrieve the user and include their saved recipes
            var user = await _context.Users.Include(u => u.SavedRecipes)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null && !user.SavedRecipes.Any(r => r.Id == recipeId))
            {
                user.SavedRecipes.Add(recipe); // Add the existing recipe object
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Recipe>> GetSavedRecipesAsync(Guid userId)
        {
            var user = await _context.Users.Include(u => u.SavedRecipes)
                                           .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.SavedRecipes ?? new List<Recipe>();
        }
    }
}