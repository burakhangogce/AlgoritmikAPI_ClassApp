using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class MyClass
    {
        [Key]
        public int classId { get; set; }
        public int classTeacherId { get; set; }
        public string className { get; set; } 
        public int classLevel { get; set; }
        
    }
}
