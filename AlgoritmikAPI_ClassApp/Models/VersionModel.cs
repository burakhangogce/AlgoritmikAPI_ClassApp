using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class VersionModel
    {
        [Key]
        public int versionId { get; set; }
        public string version { get; set; }
        public int enable { get; set; }
    }
}
