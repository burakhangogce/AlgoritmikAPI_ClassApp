using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class InviteRepository : IInvite
    {
        readonly DatabaseContext _dbContext = new();

        public InviteRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public InviteModel GetInvite(string inviteCode)
        {
            try
            {
                InviteModel? invite = _dbContext.Invites!.Find(inviteCode);
                if (invite != null)
                {

                    return invite;
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

        public List<InviteModel> GetInvites(int nutritionistId)
        {
            try
            {
                List<InviteModel> inviteModels = _dbContext.Invites!.Where(x => x.inviteNutritionistId.Equals(nutritionistId)).ToList();
                if (inviteModels != null)
                {
                    return inviteModels;
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

        public void AddInvite(InviteModel inviteModel)
        {
            try
            {
                _dbContext.Invites!.Add(inviteModel);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

    }
}
