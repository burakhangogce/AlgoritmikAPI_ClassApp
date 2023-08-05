using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IUser
    {
        public UserInfo GetUserWithId(int id);
        public UserInfo GetUserWithEmail(string email);
    }
}
