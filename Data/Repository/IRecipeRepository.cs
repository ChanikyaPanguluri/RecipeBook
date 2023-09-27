using RecipeBook.Models;

namespace RecipeBook.Data.Repository
{
    public interface IRecipeRepository
    {
        int AddRecipe(Recipe recipe);
        int deleteRecipe(int id);
        List<Recipe> GetRecipes();
        Recipe? getRecipeById(int id);
        int updateRecipe(Recipe recipe);
       
    }
}