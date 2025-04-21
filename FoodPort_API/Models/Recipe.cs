using System.Text.Json.Serialization;

namespace FoodPort_API.Models
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Guid AuthorId { get; set; } = Guid.Empty; // Name of the user who created the recipe
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Cooking details
        public int PrepTime { get; set; } // in minutes
        public int Servings { get; set; }
        public Difficulty Difficulty { get; set; }

        // Ingredients & Instructions
        public List<Ingredient> Ingredients { get; set; } = new();
        public List<Instruction> Instructions { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();

        // Nutrition Facts
        public NutritionFacts Nutrition { get; set; } = new();

        // User Engagement
        public List<Like> Likes { get; set; } = new();
        public List<Saves> Saves { get; set; } = new();
       // public List<Tag> Tags { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();

    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Difficulty 
    {
        Easy,
        Medium,
        Hard
    }
}
