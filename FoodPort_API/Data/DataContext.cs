using FoodPort_API.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodPort_API.Data
{
    public class  DataContext : DbContext
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

    }
}
