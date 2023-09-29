namespace RecipeBook.Models
{
    public class RecipeDTO
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public List<IngredientDTO> Ingredients { get; set;}

    }

    public class IngredientDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

        public int? RecipeId { get; set; }
    }
}
