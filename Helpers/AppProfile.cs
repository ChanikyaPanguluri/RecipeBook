using AutoMapper;
using RecipeBook.Models;

namespace RecipeBook.Helpers
{
    public class AppProfile :Profile
    {
        public AppProfile()
        {
            CreateMap<RecipeDTO, Recipe>();
            CreateMap<IngredientDTO, Ingredient>();
        }

    }
}
