using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class NotificationRepository : INotification
    {
        readonly DatabaseContext _dbContext = new();

        public NotificationRepository(DatabaseContext dbContext)
        { 
            _dbContext = dbContext;
        }
       

        public List<NotificationModel> FindDietDayMenu()
        {
            try
            {
                DateTime nowDate = DateTime.Now;
                List<DietMenuModel> dietDayMenuList = _dbContext.DietDayMenus!.Where(x => x.dietMenuTime < nowDate).ToList();


                if (dietDayMenuList != null)
                {
                    List<NotificationModel> notificationList = new List<NotificationModel>();

                    for (int i = 0; i < dietDayMenuList.Count; i++)
                    {
                        DietDayModel? dietDayModel = _dbContext.DietDays!.Find(dietDayMenuList[i].dietDayId);
                        DietModel? dietModel = _dbContext.Diets!.Find(dietDayModel!.dietId);
                        ClientModel clientModel = _dbContext.Client!.Find(dietModel!.clientId)!;
                        NotificationModel notificationModel = new NotificationModel { client = clientModel, notificationMessage = dietDayMenuList[i].dietMenuTitle, notificationDate = dietDayMenuList[i].dietMenuTime};
                        notificationList.Add(notificationModel);
                    }



                    return notificationList;
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

       
    }
}
