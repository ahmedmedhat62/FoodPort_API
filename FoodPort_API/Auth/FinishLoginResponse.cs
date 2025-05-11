namespace FoodPort_API.Auth
{
    public class FinishLoginResponse
    {
        public Guid Userid { get; set; }
        public bool UserExisted { get; set; }
    }
}
