using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IHomeWork
    {
        public List<HomeWork> GetHomeWorkDetails();
        public HomeWork GetHomeWorkDetails(int id);
        public void AddHomeWork(HomeWork homework);
        public void UpdateHomeWork(HomeWork homework);
        public HomeWork DeleteHomeWork(int id);
        public bool CheckHomeWork(int id);

    }
}
