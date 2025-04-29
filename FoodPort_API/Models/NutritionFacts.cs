namespace FoodPort_API.Models
{
    public class NutritionFacts
    {
        public Guid Id { get; set; }
        public Guid Recipe_Id { get; set; }
        public double fat_total { get; set; }
        public double Fat_saturated { get; set; } // in grams
        public double Carbohydrates { get; set; } // in grams
        public double fiber { get; set; } // in grams
        public double Sugar { get; set; } // in grams
    }
}
