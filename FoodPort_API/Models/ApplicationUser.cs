using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodPort_API.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
       
        public string ProfilePicture { get; set; } = string.Empty;
        [NotMapped] 
        public ICollection<Guid> Pantry { get; set; } = new List<Guid>();
     
        public ICollection<Recipe> SavedRecipes { get; set; } = new List<Recipe>();
        [NotMapped] 
        public ICollection<Guid> PostedRecipes { get; set; } = new List<Guid>();
        [NotMapped]
        public ICollection<Guid> Followers { get; set; } = new List<Guid>();
        [NotMapped]
        public ICollection<Guid> Following { get; set; } = new List<Guid>();
        
    }
}
