using RecipeBook.Models;

namespace RecipeBook.Data.Repository
{
    public class IngredientsRepository : IIngredientsRepository
    {
        private readonly ApplicationDbContext _db;
        public IngredientsRepository(ApplicationDbContext db)
        {
            _db = db;

        }

        public List<Ingredient> GetAllIngredients()
        {
            try
            {
                var allRecords = _db.Ingredients.ToList();
                if (allRecords == null)
                {
                    return new List<Ingredient>();
                }
                return allRecords;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddIngredients(Ingredient value)
        {
            try
            {
                if (value.Id != null)
                {
                    var checkForExisting = _db.Ingredients.FirstOrDefault(x => x.Name == value.Name);
                    if (checkForExisting == null)
                    {
                        _db.Ingredients.Add(value);
                        var res = _db.SaveChanges();
                        return res;
                    }
                    else
                    {
                        checkForExisting.Count += value.Count;
                        _db.Ingredients.Update(checkForExisting);
                        var res=_db.SaveChanges();
                        return res;
                    }
                }
                return 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public int updateIngredients(Ingredient ing)
        {
            try
            {
                //var existingRecord = _db.Ingredients.FirstOrDefault(x => x.Id == ing.Id);
                if (ing.Id != null)
                {
                    _db.Ingredients.Update(ing);
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

        public List<Ingredient> GetIngredientsbyRecipe(int id)
        {
            try
            {
                var ingredient = GetAllIngredients().Where(x => x.Id == id).ToList();
                return ingredient;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int deleteIngredients(int id)
        {
            try
            {
                //var ingredient= GetAllIngredients().FirstOrDefault(x=>x.RecipeId==);
                var existingRecord = _db.Ingredients.Find(id);
                if (existingRecord != null)
                {
                    _db.Ingredients.Remove(existingRecord);
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
