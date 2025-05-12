namespace FoodPort_API.Models
{
    public class UserFollower
    {
        public Guid FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }

        public Guid FollowedId { get; set; }
        public ApplicationUser Followed { get; set; }
    }
}
