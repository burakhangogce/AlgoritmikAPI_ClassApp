using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IVersion
    {
        public VersionModel GetVersion(int id);
        public List<VersionModel> GetVersions();

    }
}
