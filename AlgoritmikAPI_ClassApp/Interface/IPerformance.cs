using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IPerformance
    {
        public List<Performance> GetPerformanceDetails();
        public Performance GetPerformanceDetails(int id);
        public void AddPerformance(Performance performance);
        public void UpdatePerformance(Performance performance);
        public Performance DeletePerformance(int id);
        public bool CheckPerformance(int id);
    }
}
