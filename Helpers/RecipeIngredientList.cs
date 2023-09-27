using RecipeBook.Models;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Helpers
{
    public class RecipeIngredientList
    {
        public int RecipeId { get; set; }
        
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? ImagePath { get; set; }

        public List<IngredientModel> Ingredients { get; set; }
    }

    public class IngredientModel
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
