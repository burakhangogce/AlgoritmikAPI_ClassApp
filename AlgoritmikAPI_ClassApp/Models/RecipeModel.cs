using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class RecipeModel
    {
        [Key]
        public int recipeId { get; set; }
        public int nutritionistId { get; set; }
        public string recipeTitle { get; set; }
        public string recipeDesc { get; set; }
        [ForeignKey("nutritionistId")]
        public virtual NutritionistModel? nutritionistModel { get; set; }

    }
}
