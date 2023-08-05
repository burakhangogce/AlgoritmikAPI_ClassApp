using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IInvite
    {
        public InviteModel GetInvite(string inviteCode);
        public List<InviteModel> GetInvites(int nutritionistId);
        public void AddInvite(InviteModel inviteModel);

    }
}
