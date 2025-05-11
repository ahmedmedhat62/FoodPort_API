using System.ComponentModel.DataAnnotations;

namespace FoodPort_API.Models.DTOs
{
    public class LoginDTO
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
