using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class HomeWorkRepository : IHomeWork
    {
        readonly DatabaseContext _dbContext = new();

        public HomeWorkRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<HomeWork> GetHomeWorkDetails()
        {
            try
            {
                return _dbContext.HomeWork.ToList();
            }
            catch
            {
                throw;
            }
        }

        public HomeWork GetHomeWorkDetails(int id)
        {
            try
            {
                HomeWork? homeWork = _dbContext.HomeWork.Find(id);
                if (homeWork != null)
                {
                    return homeWork;
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

        public void AddHomeWork(HomeWork homeWork)
        {
            try
            {
                _dbContext.HomeWork.Add(homeWork);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateHomeWork(HomeWork homeWork)
        {
            try
            {
                _dbContext.Entry(homeWork).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public HomeWork DeleteHomeWork(int id)
        {
            try
            {
                HomeWork? homeWork = _dbContext.HomeWork.Find(id);

                if (homeWork != null)
                {
                    _dbContext.HomeWork.Remove(homeWork);
                    _dbContext.SaveChanges();
                    return homeWork;
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

        public bool CheckHomeWork(int id)
        {
            return _dbContext.HomeWork.Any(e => e.homeWorkId == id);
        }
    }
}
