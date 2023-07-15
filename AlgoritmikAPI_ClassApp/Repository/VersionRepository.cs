using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class VersionRepository : IVersion
    {
        readonly DatabaseContext _dbContext = new();

        public VersionRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public VersionModel GetVersion(int id)
        {
            try
            {
                VersionModel? versionModel = _dbContext.Versions!.Find(id);
                if (versionModel != null)
                {
                    return versionModel;
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



        public List<VersionModel> GetVersions()
        {
            try
            {
                List<VersionModel> versionList = _dbContext.Versions.ToList();
                if (versionList != null)
                {
                    return versionList;
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
