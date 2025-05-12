using FoodPort_API.Models;
using FoodPort_API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodPort_API.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<NutritionFacts> NutritionFacts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Saves> Saves { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<UserFollower> UserFollowers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: Configure FK constraints if needed explicitly
            base.OnModelCreating(modelBuilder);

            // Configure users following other users
            modelBuilder.Entity<UserFollower>()
                .HasKey(uf => new { uf.FollowerId, uf.FollowedId });

            modelBuilder.Entity<UserFollower>()
                .HasOne(uf => uf.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(uf => uf.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserFollower>()
                .HasOne(uf => uf.Followed)
                .WithMany(u => u.Followers)
                .HasForeignKey(uf => uf.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure other relationships as needed
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.SavedRecipes)
                .WithMany()
                .UsingEntity(join => join.ToTable("UserSavedRecipes"));

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.PostedRecipes)
                .WithOne()
                .HasForeignKey(r => r.AuthorId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
