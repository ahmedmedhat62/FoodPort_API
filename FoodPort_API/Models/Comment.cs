namespace FoodPort_API.Models
{
    public class Comment
    {
        public Guid Id { get; set; } 
        public Guid AuthorId { get; set; } 
        public Guid RecipeId { get; set; }
        public string Content { get; set; } 
        public DateTime PostedAt { get; set; } = DateTime.UtcNow;
    }
}
