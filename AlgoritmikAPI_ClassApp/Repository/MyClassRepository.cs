﻿using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class MyClassRepository : IMyClass
    {
        readonly DatabaseContext _dbContext = new();

        public MyClassRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MyClass> GetMyClassDetails()
        {
            try
            {
                return _dbContext.MyClasses.ToList();
            }
            catch
            {
                throw;
            }
        }

        public MyClass GetMyClassDetails(int id)
        {
            try
            {
                MyClass? myclass = _dbContext.MyClasses.Find(id);
                if (myclass != null)
                {
                    return myclass;
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

        public void AddMyClass(MyClass myclass)
        {
            try
            {
                _dbContext.MyClasses.Add(myclass);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateMyClass(MyClass myclass)
        {
            try
            {
                _dbContext.Entry(myclass).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public MyClass DeleteMyClass(int id)
        {
            try
            {
                MyClass? myclass = _dbContext.MyClasses.Find(id);

                if (myclass != null)
                {
                    _dbContext.MyClasses.Remove(myclass);
                    _dbContext.SaveChanges();
                    return myclass;
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

        public bool CheckMyClass(int id)
        {
            return _dbContext.MyClasses.Any(e => e.classId == id);
        }
    }
}
