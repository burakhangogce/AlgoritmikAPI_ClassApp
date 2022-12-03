using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class ClientModel
    {
        [Key]
        public int clientId { get; set; }
        public int nutritionistId { get; set; }
        public DateTime clientStartDate { get; set; }
        public string clientName { get; set; }
        public int clientAge { get; set; }
        public int firstWeight { get; set; }
        public int lastWeight { get; set; }
        public string token { get; set; }
        [ForeignKey("nutritionistId")]
        public virtual NutritionistModel? nutritionistModel { get; set; }
    }
}
