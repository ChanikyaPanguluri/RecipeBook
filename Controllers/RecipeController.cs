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
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _repo;
        private readonly IMapper _mapper;
        public RecipeController(IRecipeRepository recipeRepository,IMapper mapper)
        {
            _repo = recipeRepository;
            _mapper = mapper;
        }

        #region Get All Recipes
        [HttpGet]
        [Route("get")]
        public IActionResult getAllRecipes()
        {
            try
            {
                var allRecipes = _repo.GetRecipes().ToList();
                if (allRecipes == null)
                {
                    return StatusCode(200, allRecipes);
                }
                return StatusCode(200, allRecipes);
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
        public IActionResult getRecipeBYId(int id) 
        {
            try
            {
                var filteredRecipe = _repo.getRecipeById(id);
                return StatusCode(200, filteredRecipe);
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
        public IActionResult addRecipe(RecipeDTO recipeload)
        {
            try
            {

                var newRecipe = _mapper.Map<Recipe>(recipeload);
                var statusMessage = ( _repo.AddRecipe(newRecipe) > 0 ? "Added Success" : "Failed To Add");
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
        public IActionResult editRecipe(RecipeDTO recipe)
        {
            try
            {
                var newRecipe = _mapper.Map<Recipe>(recipe);
                var statusMessage = (_repo.updateRecipe(newRecipe) > 0 ? "UpdateSuccess" : "Failed To Update");
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
        public IActionResult deleterecipe([FromRoute] int id)
        {
            try
            {

                var statusMessage = (_repo.deleteRecipe(id) > 0 ? "DeleteSuccess" : "Failed To Delete");
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
