using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class DietMenuModel
    {
        [Key]
        public int dietMenuId { get; set; }
        public int dietDayId { get; set; }
        public string dietMenuTitle { get; set; }
        public string dietMenuDetail { get; set; }
        public DateTime? dietMenuTime { get; set; }
        public bool isNotification { get; set; }
        public bool isCompleted { get; set; }
        [ForeignKey("dietDayId")]
        public virtual DietDayModel? dietDayModel { get; set; }
        public virtual int isDelete { get; set; }
    }
}
