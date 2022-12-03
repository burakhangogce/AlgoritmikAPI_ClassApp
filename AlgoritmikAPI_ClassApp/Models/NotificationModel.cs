using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class NotificationModel
    {
        public ClientModel? client;
        public string notificationMessage;
        public DateTime? notificationDate;
       
    }
}
