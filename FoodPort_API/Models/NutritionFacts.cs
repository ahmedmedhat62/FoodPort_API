namespace FoodPort_API.Models
{
    public class NutritionFacts
    {
        public Guid Id { get; set; }
        public Guid Recipe_Id { get; set; }
        public int Calories { get; set; }
        public int Fat { get; set; } // in grams
        public int Carbohydrates { get; set; } // in grams
        public int Protein { get; set; } // in grams
        public int Sugar { get; set; } // in grams
    }
}
