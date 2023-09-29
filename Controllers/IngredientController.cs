using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecipeBook.Data.Repository;
using RecipeBook.Helpers;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientsRepository _repo;
        private readonly IMapper _mapper;
        public IngredientController(IIngredientsRepository ingredientRepository,IMapper mapper)
        {
            _repo = ingredientRepository;
            _mapper = mapper;
        }

        #region Get All Recipes
        [HttpGet]
        [Route("get")]
        public IActionResult getAllIngredients()
        {
            try
            {
                var allIngredients = _repo.GetAllIngredients().ToList();
                if (allIngredients == null)
                {
                    return StatusCode(200, allIngredients);
                }
                return StatusCode(200, allIngredients);
            }
            catch (Exception ex)
            {

               return StatusCode(500,ex.Message);
            }
        }

        #endregion

        #region filter Recipes By Id
        [HttpGet]
        [Route("get/{id}")]
        public IActionResult getIngredientBYId(int id) 
        {
            try
            {
                var filteredIngredients = _repo.GetIngredientsbyRecipe(id);
                return StatusCode(200, filteredIngredients);
            }
            catch (Exception ex)
            {

                return StatusCode(404, ex.Message);
            }
        }
        #endregion

        #region Add recipe 
        [HttpPost]
        [Route("add")]
        public IActionResult addIngredient(IngredientDTO ingredients)
        {
            try
            {

                var newIngredient = _mapper.Map<Ingredient>(ingredients);
                var statusMessage = ( _repo.AddIngredients(newIngredient) > 0 ? "Added Success" : "Failed To Add");
                return StatusCode(201,statusMessage);
            }
            catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }

        }

        #endregion


        #region Edit Recipe

        [HttpPut]
        [Route("edit")]
        public IActionResult editIngredient(IngredientDTO ingredient)
        {
            try
            {
                var newIngredient = _mapper.Map<Ingredient>(ingredient);
                var statusMessage = (_repo.updateIngredients(newIngredient) > 0 ? "UpdateSuccess" : "Failed To Update");
                return StatusCode(202, statusMessage);
            }
            catch (Exception ex)
            {

                return StatusCode(204, ex.Message);
            }
        }


        #endregion

        #region Delete Recipe

        [HttpDelete]
        [Route("{id}")]
        public IActionResult deleteIngredient([FromRoute] int id)
        {
            try
            {

                var statusMessage = (_repo.deleteIngredients(id) > 0 ? "DeleteSuccess" : "Failed To Delete");
                return StatusCode(202, statusMessage);
            }
            catch (Exception ex)
            {

                return StatusCode(204, ex.Message);
            }
        }

        #endregion
    }
}
