using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class DietDayModel
    {
        [Key]
        public int dietDayId { get; set; }
         [ForeignKey("dietId")]
        public int dietId { get; set; }
        public DateTime dietTime { get; set; }
        public List<DietMenuModel> dietMenus { get; set; }

    }
}
