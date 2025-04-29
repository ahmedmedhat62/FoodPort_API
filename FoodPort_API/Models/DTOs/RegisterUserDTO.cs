namespace FoodPort_API.Models.DTOs
{
    public class RegisterUserDTO
    {
        // Required fields for registration
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        // Optional fields for profile setup
        public string Name { get; set; }

        public string ProfilePictureUrl { get; set; } // URL or file path for profile picture

        public List<TagDTO> PreferredTags { get; set; } = new List<TagDTO>(); // Tag names
    }
}
