using System.ComponentModel.DataAnnotations;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class ClientModel
    {
        [Key]
        public int clientId { get; set; }
        public DateTime clientStartDate { get; set; }
        public string clientName { get; set; }
        public string clientAge { get; set; }
        public int firstWeight { get; set; }
        public int lastWeight { get; set; }
        public DietModel dietModel { get; set; }
    }
}
