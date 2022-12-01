using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public async Task<ActionResult<IEnumerable<RecipeModel>>> Get(int id)
        {
            List<RecipeModel> recipeList = await Task.FromResult(_IRecipe.GetRecipes(id));

            if (recipeList == null)
            {
                return NotFound();
            }
            return Ok(recipeList);
        }

        [HttpPost]
        public async Task<ActionResult<RecipeModel>> Post(RecipeModel recipeModel)
        {
            _IRecipe.AddRecipe(recipeModel);
            return await Task.FromResult(recipeModel);
        }



        [HttpPut("update/{id}")]
        public async Task<ActionResult<RecipeModel>> Put(int id, RecipeModel recipeModel)
        {
            if (id != recipeModel.recipeId)
            {
                return BadRequest();
            }
            try
            {
                _IRecipe.UpdateRecipe(recipeModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(recipeModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<RecipeModel>> Delete(int id)
        {
            var recipe = _IRecipe.DeleteRecipe(id);
            return await Task.FromResult(recipe);
        }

        private bool RecipeExists(int id)
        {
            return _IRecipe.CheckRecipe(id);
        }
    }
}
