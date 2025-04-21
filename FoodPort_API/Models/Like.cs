namespace FoodPort_API.Models
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RecipeId { get; set; }
        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    }
}
