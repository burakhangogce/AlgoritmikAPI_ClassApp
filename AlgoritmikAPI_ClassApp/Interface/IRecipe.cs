using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IRecipe
    {
        public List<RecipeModel> GetRecipes(int id);
        public void AddRecipe(RecipeModel recipeModel);
        public void UpdateRecipe(RecipeModel recipeModel);
        public RecipeModel DeleteRecipe(int id);
        public bool CheckRecipe(int id);

    }
}
