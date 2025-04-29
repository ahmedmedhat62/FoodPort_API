using FoodPort_API.Models.DTOs;
using FoodPort_API.Models;
using FoodPort_API.Data;
using Microsoft.AspNetCore.Identity;
using FoodPort_API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodPort_API.Interfaces
{
    public interface IUser
    {
        Task<ApplicationUser> RegisterUserAsync(RegisterUserDTO registerDto);
        Task<ApplicationUser> GetUserByIdAsync(Guid userId);
        Task UpdateUserProfileAsync(Guid userId, UpdateUserProfileDTO updateDto);
        Task UploadProfilePictureAsync(Guid userId, IFormFile image);
    }
}
public class UserService : IUser
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly DataContext _context;

    public UserService(UserManager<ApplicationUser> userManager, DataContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<ApplicationUser> RegisterUserAsync(RegisterUserDTO registerDto)
    {
        var user = new ApplicationUser
        {
            UserName = registerDto.Username,
            Email = registerDto.Email,
            Name = registerDto.Name,
            ProfilePicture = registerDto.ProfilePictureUrl
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Map preferred tags
        foreach (var tagName in registerDto.PreferredTags)
        {
            var tag = new Tag { Name = tagName.Name };
            user.PreferredTags.Add(tag);
        }

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<ApplicationUser> GetUserByIdAsync(Guid userId)
    {
        return await _context.Users
            .Include(u => u.PreferredTags)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task UpdateUserProfileAsync(Guid userId, UpdateUserProfileDTO updateDto)
    {
        var user = await GetUserByIdAsync(userId);
        if (user == null) throw new Exception("User not found.");

        user.Name = updateDto.Name;
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
}