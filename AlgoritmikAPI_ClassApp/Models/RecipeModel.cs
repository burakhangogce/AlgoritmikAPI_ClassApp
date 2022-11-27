using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class RecipeModel
    {
        [Key]
        public int recipeId { get; set; }
        public string recipeTitle { get; set; }
        public string recipeDesc { get; set; }
    }
}
