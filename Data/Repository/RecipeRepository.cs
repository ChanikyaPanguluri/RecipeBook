using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;

namespace RecipeBook.Data.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _db;
        public RecipeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<Recipe> GetRecipes()
        {
            try
            {
                var res = _db.Recipes.Include(_ => _.Ingredients).ToList();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Recipe? getRecipeById(int id)
        {
            try
            {
                var recipe = GetRecipes().Where(x => x.Id == id).FirstOrDefault();
                return recipe;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddRecipe(Recipe recipe)
        {
            try
            {
                if (recipe.Id != null)
                {
                    var checkForExisting = _db.Recipes.FirstOrDefault(_ => _.Id == recipe.Id);
                    if (checkForExisting == null)
                    {
                        _db.Recipes.Add(recipe);

                        var res = _db.SaveChanges();

                        return res;
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return 0;
        }

        public int updateRecipe(Recipe recipe)
        {
            try
            {

                _db.Recipes.Update(recipe);
                var res = _db.SaveChanges();
                return res;
            }
            catch (Exception)
            {

                throw;
            }
            
            
        }

        public int deleteRecipe(int id)
        {
            try
            {
                var checkForExisting = _db.Recipes.Find(id);

                if (checkForExisting != null)
                {
                    _db.Recipes.Remove(checkForExisting);
                    var res = _db.SaveChanges();
                    return res;
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

     
    }
}
