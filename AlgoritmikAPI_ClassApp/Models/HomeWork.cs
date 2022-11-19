using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class HomeWork
    {
        [Key]
        public int homeWorkId { get; set; }
        public int homeWorkClassId { get; set; }
        public int homeWorkTeacherId { get; set; }
        public string homeWorkName { get; set; } 
        public string homeWorkDesc { get; set; }
        public DateTime homeWorkDate { get; set; }

    }
}
