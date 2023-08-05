using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class NutritionistModel
    {

        [Key]
        public int nutritionistId { get; set; }
        public int userId { get; set; }
        public string nutritionistName { get; set; }
        public string nutritionistTitle { get; set; }
        public int nutritionistAge { get; set; }
        public int fitimCoin { get; set; }
        [ForeignKey("userId")]
        public virtual UserInfo? userModel { get; set; }

    }
}
