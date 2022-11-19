using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IReport
    {
        public List<Report> GetReportDetails();
        public Report GetReportDetails(int id);
        public void AddReport(Report report);
        public void UpdateReport(Report report);
        public Report DeleteReport(int id);
        public bool CheckReport(int id);

    }
}
