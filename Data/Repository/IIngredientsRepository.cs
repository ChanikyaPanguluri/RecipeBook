using RecipeBook.Models;

namespace RecipeBook.Data.Repository
{
    public interface IIngredientsRepository
    {
        int AddIngredients(Ingredient value);
        int deleteIngredients(int id);
        List<Ingredient> GetAllIngredients();
        List<Ingredient> GetIngredientsbyRecipe(int id);
        int updateIngredients(Ingredient ing);
    }
}