using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class PerformanceRepository : IPerformance
    {
        readonly DatabaseContext _dbContext = new();

        public PerformanceRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Performance> GetPerformanceDetails()
        {
            try
            {
                return _dbContext.Performance.ToList();
            }
            catch
            {
                throw;
            }
        }

        public Performance GetPerformanceDetails(int id)
        {
            try
            {
                Performance? performance = _dbContext.Performance.Find(id);
                if (performance != null)
                {
                    return performance;
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

        public void AddPerformance(Performance performance)
        {
            try
            {
                _dbContext.Performance.Add(performance);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePerformance(Performance performance)
        {
            try
            {
                _dbContext.Entry(performance).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Performance DeletePerformance(int id)
        {
            try
            {
                Performance? performance = _dbContext.Performance.Find(id);

                if (performance != null)
                {
                    _dbContext.Performance.Remove(performance);
                    _dbContext.SaveChanges();
                    return performance;
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

        public bool CheckPerformance(int id)
        {
            return _dbContext.Performance.Any(e => e.performanceId == id);
        }
    }
}
