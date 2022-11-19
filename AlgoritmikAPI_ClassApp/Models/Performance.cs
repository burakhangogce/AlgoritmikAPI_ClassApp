using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class Performance
    {
        [Key]
        public int performanceId { get; set; }
        public int performanceStudentId { get; set; }
        public DateTime performanceDate { get; set; }
    }
}
