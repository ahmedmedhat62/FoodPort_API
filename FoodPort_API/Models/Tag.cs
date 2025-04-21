namespace FoodPort_API.Models
{
    public class Tag
    {
        public Guid Id { get; set; } 
        public Guid Recipe_Id { get; set; }
        public string Name { get; set; } 
    }
}
