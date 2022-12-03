using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        readonly DatabaseContext _dbContext = new();

        private readonly INotification _INotification;

        public NotificationController(INotification INotification, DatabaseContext dbContext)
        {
            _dbContext = dbContext;

            _INotification = INotification;
        }


        [HttpGet("sendNotification")]
        public async Task<ActionResult<DietMenuModel>> GetNotificationService()
        {
            List<NotificationModel> notificationList = await Task.FromResult(_INotification.FindDietDayMenu());


            if (notificationList == null)
            {
                return NotFound();
            }

            for (int i = 0; i<notificationList.Count; i++)
            {

                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAA4Z3Au2k:APA91bHyFQQEzJqgA2h9_38vFZ8S15OvZBskhKiMrXuF2znsROH7BCnrUxiskPG6dsDLmKz44YpSj4GcayJ2xpiNgvKMZDcU5fc7P0d1P6GCclVTrUzOmB223CMZDwBwKxte_aXgJ-vT"));
                //Sender Id - From firebase project setting  
                tRequest.Headers.Add(string.Format("Sender: id={0}", "969014295401"));
                tRequest.ContentType = "application/json";

                var payload = new
                {
                    to = notificationList[i].client!.token,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = notificationList[i].notificationDate.ToString(),
                        title = notificationList[i].notificationMessage.ToString(),
                        badge = 1
                    },
                    data = new
                    {
                        key1 = "value1",
                        key2 = "value2"
                    }
                };

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                }
                        }
                    }
                }
            }
            return Ok();

        }


    }
}
