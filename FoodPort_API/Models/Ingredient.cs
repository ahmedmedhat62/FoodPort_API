using System.Text.Json.Serialization;

namespace FoodPort_API.Models
{
    public class Ingredient
    {
        public Guid Id { get; set; }
        public Guid Recipe_Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Quantity { get; set; } // Numeric value
        public MeasurementUnit Unit { get; set; } // Unit of measurement
        public double WeightInGrams { get; set; }
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MeasurementUnit
    {
        Grams,
        Kilograms,
        Liters,
        Amount // Generic count, like 1 apple or 2 eggs
    }
    //Client ID: c890240ec5cc4eecb8a9f34d0c82e41e Client Secret: b34e494bb35a4dbdb278514fd6f27949

}
