using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class DietModel
    {
        public int clientId { get; set; }
        [Key]
        public int dietId { get; set; }
        public string dietTitle { get; set; }
        public DateTime dietStartDate { get; set; }
        public DateTime dietEndDate { get; set; }

        public List<DietDayModel> dietDayModel { get; set; }
    }
}
