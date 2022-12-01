using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class RecipeRepository : IRecipe
    {
        readonly DatabaseContext _dbContext = new();

        public RecipeRepository(DatabaseContext dbContext)
        { 
            _dbContext = dbContext;
        }
       

        public List<RecipeModel> GetRecipes(int id)
        {
            try
            {
                List<RecipeModel> recipeList = _dbContext.Recipe!.Where(x => x.nutritionistId.Equals(id)).ToList();
                if (recipeList != null)
                {
                    return recipeList;
                }
                else
                {
                    throw new ArgumentNullException();
                }

            }
            catch
            {
                throw;
            }
        }


        public void AddRecipe(RecipeModel recipeModel)
        {
            try
            {
                _dbContext.Recipe!.Add(recipeModel);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateRecipe(RecipeModel recipeModel)
        {
            try
            {
                _dbContext.Recipe!.Update(recipeModel);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public RecipeModel DeleteRecipe(int id)
        {
            try
            {
                RecipeModel? recipeModel = _dbContext.Recipe.Find(id);

                if (recipeModel != null)
                {
                    _dbContext.Recipe.Remove(recipeModel);
                    _dbContext.SaveChanges();
                    return recipeModel;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool CheckRecipe(int id)
        {
            return _dbContext.Recipe.Any(e => e.recipeId == id);
        }
    }
}
