namespace FoodPort_API.Models
{
    public class Saves
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RecipeId { get; set; }
        public DateTime SavedAt { get; set; } = DateTime.UtcNow;
    }
}
