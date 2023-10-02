using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace RecipeBook.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Recipe> Recipes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
            modelBuilder.Entity<Ingredient>()
                .HasOne(_ => _.Recipe)
                .WithMany(a => a.Ingredients)
                .HasForeignKey(a => a.RecipeId);

        }
    }
}
