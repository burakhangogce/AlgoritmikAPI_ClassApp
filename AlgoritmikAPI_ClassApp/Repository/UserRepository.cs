using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class UserRepository : IUser
    {
        readonly DatabaseContext _dbContext = new();

        public UserRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public UserInfo GetUserWithId(int id)
        {
            try
            {
                UserInfo? userInfo = _dbContext.UserInfo!.Find(id);
                if (userInfo != null)
                {
                    return userInfo;
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
        public UserInfo GetUserWithEmail(string email)
        {
            try
            {
                UserInfo userInfo = _dbContext.UserInfo!.Single(x => x.Email.Equals(email));
                if (userInfo != null)
                {
                    return userInfo;
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
