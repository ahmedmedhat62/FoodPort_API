using System.Text.Json.Serialization;

namespace FoodPort_API.Models.DTOs
{
    public class IngredientDTO
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public MeasurementUnit Unit { get; set; }
    }
    
}
