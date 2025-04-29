using Microsoft.AspNetCore.Identity;

namespace FoodPort_API.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public ICollection<Ingredient> Pantry { get; set; } = new List<Ingredient>();
        public ICollection<Recipe> SavedRecipes { get; set; } = new List<Recipe>();
        public ICollection<Recipe> PostedRecipes { get; set; } = new List<Recipe>();
        public ICollection<ApplicationUser> Followers { get; set; } = new List<ApplicationUser>();
        public ICollection<ApplicationUser> Following { get; set; } = new List<ApplicationUser>();
        public ICollection<Tag> PreferredTags { get; set; } = new List<Tag>();
    }
}
