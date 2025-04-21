using System.ComponentModel.DataAnnotations;

namespace FoodPort_API.Models.DTOs
{
    public class CreateRecipeDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }


        public Guid AuthorId { get; set; }

        public int PrepTime { get; set; }
        public int Servings { get; set; }
        public Difficulty Difficulty { get; set; }


        public IFormFile Image { get; set; } // 📷 Uploaded image file

        public List<IngredientDTO> Ingredients { get; set; } 
        public List<InstructionsDTO> Instructions { get; set; } 
        //  public NutritionFacts Nutrition { get; set; } = new();
        public List<TagDTO> Tags { get; set; } 

    }
}
