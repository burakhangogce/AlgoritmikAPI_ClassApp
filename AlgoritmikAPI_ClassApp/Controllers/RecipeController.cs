using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/recipe")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipe _IRecipe;

        public RecipeController(IRecipe IRecipe)
        {
            _IRecipe = IRecipe;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeResponseModel>> Get(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new RecipeResponseModel(responseModel: responseModel);
            try
            {
                List<RecipeModel> recipeList = await Task.FromResult(_IRecipe.GetRecipes(id));
                if (recipeList == null || recipeList.Count <= 0)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Tarif bulunamadı.");
                    return response;
                }
                else
                {
                    response.models.AddRange(recipeList);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }

        }

        [HttpPost("addrecipe")]
        public async Task<ActionResult<RecipeResponseModel>> Post(RecipeModel recipeModel)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new RecipeResponseModel(responseModel: responseModel);
            try
            {
                _IRecipe.AddRecipe(recipeModel);
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }
            response.models.Add(recipeModel);
            return response;
        }



        [HttpPut("update/{id}")]
        public async Task<ActionResult<RecipeResponseModel>> Put(int id, RecipeModel recipeModel)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new RecipeResponseModel(responseModel: responseModel);
            try
            {
                if (id != recipeModel.recipeId)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Tarif bulunamadı!");
                    return response;
                }
                try
                {
                    _IRecipe.UpdateRecipe(recipeModel);
                    response.models.Add(recipeModel);
                    return response;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    response.statusCode = 400;
                    if (!RecipeExists(id))
                    {
                        response.isSuccess = false;
                        response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                        return response;
                    }
                    else
                    {
                        response.errorModel = new ErrorResponseModel(errorMessage: "Tarif bulunamadı!");
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.statusCode = 400;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeResponseModel>> Delete(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new RecipeResponseModel(responseModel: responseModel);
            try
            {
                var recipe = _IRecipe.DeleteRecipe(id);
                response.models.Add(recipe);
                return response;
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.statusCode = 400;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }

        }

        private bool RecipeExists(int id)
        {
            return _IRecipe.CheckRecipe(id);
        }
    }
}
