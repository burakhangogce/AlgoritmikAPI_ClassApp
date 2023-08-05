using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class InviteModel
    {
        [Key]
        public string inviteCode { get; set; }
        public int inviteNutritionistId { get; set; }
        public int inviteStatus { get; set; }
        public DateTime inviteCreateDate { get; set; }
        public string inviteDisplayName { get; set; }
    }
}
