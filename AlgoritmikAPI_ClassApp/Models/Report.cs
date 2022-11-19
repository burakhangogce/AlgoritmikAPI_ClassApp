using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class Report
    {
        [Key]
        public int reportId { get; set; }
        public int reportStudentId { get; set; }
        public string reportName { get; set; } 
        public string reportDesc { get; set; }
        
    }
}
