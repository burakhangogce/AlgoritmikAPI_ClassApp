using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class Student
    {
        [Key]
        public int studentId { get; set; }
        public int studentTeacherId { get; set; }
        public int studentClassId { get; set; }
        public int studentNumber { get; set; }
        public string studentName { get; set; }  
    }
}
