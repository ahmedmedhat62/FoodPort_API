namespace FoodPort_API.Models
{
    public class Instruction
    {
        public Guid Id { get; set; }
        public Guid Recipe_Id { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
