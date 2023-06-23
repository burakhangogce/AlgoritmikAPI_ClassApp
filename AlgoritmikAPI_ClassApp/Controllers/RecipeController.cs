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
        public async Task<ActionResult<ResponseModel<IEnumerable<RecipeModel>>>> Get(int id)
        {
            var response = new ResponseModel<IEnumerable<RecipeModel>>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                List<RecipeModel> recipeList = await Task.FromResult(_IRecipe.GetRecipes(id));
                if (recipeList == null || recipeList.Count <= 0)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Tarif bulunamadı.");
                    return response;
                }
                response.body = recipeList;
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
        public async Task<ActionResult<ResponseModel<RecipeModel>>> Post(RecipeModel recipeModel)
        {
            var response = new ResponseModel<RecipeModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
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
            response.body = await Task.FromResult(recipeModel);
            return response;
        }



        [HttpPut("update/{id}")]
        public async Task<ActionResult<ResponseModel<RecipeModel>>> Put(int id, RecipeModel recipeModel)
        {
            var response = new ResponseModel<RecipeModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
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
                    response.body = await Task.FromResult(recipeModel);
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
        public async Task<ActionResult<ResponseModel<RecipeModel>>> Delete(int id)
        {
            var response = new ResponseModel<RecipeModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                var recipe = _IRecipe.DeleteRecipe(id);
                response.body = await Task.FromResult(recipe);
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
