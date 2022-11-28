using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class ReportRepository : IReport
    {
        readonly DatabaseContext _dbContext = new();

        public ReportRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Report> GetReportDetails()
        {
            try
            {
                return _dbContext.Report.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Report GetReportDetails(int id)
        {
            try
            {
                Report? report = _dbContext.Report.Find(id);
                if (report != null)
                {
                    return report;
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

        public void AddReport(Report report)
        {
            try
            {
                _dbContext.Report.Add(report);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateReport(Report report)
        {
            try
            {
                _dbContext.Entry(report).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Report DeleteReport(int id)
        {
            try
            {
                Report? report = _dbContext.Report.Find(id);

                if (report != null)
                {
                    _dbContext.Report.Remove(report);
                    _dbContext.SaveChanges();
                    return report;
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

        public bool CheckReport(int id)
        {
            return _dbContext.Report.Any(e => e.reportId == id);
        }
    }
}
