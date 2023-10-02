using Microsoft.EntityFrameworkCore;
using RecipeBook.Models;
using System.Diagnostics;

namespace RecipeBook.Data.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IIngredientsRepository _ingRepo;
        public RecipeRepository(ApplicationDbContext db, IIngredientsRepository ingRepo)
        {
            _db = db;
            _ingRepo = ingRepo;

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
                        var ingredients = recipe.Ingredients.ToList();
                        recipe.Ingredients = new List<Ingredient>();
                        _db.Recipes.Add(recipe);

                        var res = _db.SaveChanges();
                        int recipeId = recipe.Id;
                        foreach (var item in ingredients)
                        {
                            item.RecipeId = recipeId;
                            _ingRepo.AddIngredients(item);

                        }
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
                _db.ChangeTracker.AutoDetectChangesEnabled = false;
                var ingredients = recipe.Ingredients;
                recipe.Ingredients = new List<Ingredient>();
                _db.Recipes.Update(recipe);
                var res = _db.SaveChanges();

                foreach (var item in ingredients)
                {

                    var allRecipeIngredientsFromDB = _db.Ingredients.Where(x=>x.RecipeId == recipe.Id).ToList();
                    var ingredientsToBeDeleted = allRecipeIngredientsFromDB.Where(x => !ingredients.Any(y => y.Id == x.Id)).ToList();
                   
                    if(allRecipeIngredientsFromDB.FirstOrDefault(x=>x.Id == item.Id && x.RecipeId == item.RecipeId) != null)
                    {
                        _ingRepo.updateIngredients(item);
                    }
                    else
                    {
                        item.RecipeId = recipe.Id;
                        _ingRepo.AddIngredients(item);
                    }

                    foreach (var item1 in ingredientsToBeDeleted)
                    {
                        _ingRepo.deleteIngredients(item1.Id);
                    }

                }
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
                var existingIngredients = _db.Ingredients.Where(x => x.RecipeId == id).ToList();
                if(existingIngredients!=null)
                {
                    foreach (var item in existingIngredients)
                    {
                        _ingRepo.deleteIngredients(item.Id);
                    }
                }
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
