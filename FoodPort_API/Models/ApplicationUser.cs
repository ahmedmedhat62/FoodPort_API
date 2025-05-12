using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks.Dataflow;

namespace FoodPort_API.Models
{
    public class ApplicationUser: IdentityUser<Guid>
    {
        public string bio {get;set;} 
        public DateTime joindated { get;set;}
        public string ProfilePicture { get; set; } = string.Empty;
         
        public ICollection<Ingredient> Pantry { get; set; } = new List<Ingredient>();
     
        public ICollection<Recipe> SavedRecipes { get; set; } = new List<Recipe>();
         
        public ICollection<Recipe> PostedRecipes { get; set; } = new List<Recipe>();
        public ICollection<UserFollower> Following { get; set; } = new List<UserFollower>();
        public ICollection<UserFollower> Followers { get; set; } = new List<UserFollower>();

    }
}
