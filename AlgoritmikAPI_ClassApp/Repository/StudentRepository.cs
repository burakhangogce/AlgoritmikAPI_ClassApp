using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class StudentRepository : IStudent
    {
        readonly DatabaseContext _dbContext = new();

        public StudentRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Student> GetStudentDetails()
        {
            try
            {
                return _dbContext.Student.ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<Student> GetStudentsWithClassId(int classId)
        {
            try
            {
                return _dbContext.Student.Where(x => x.studentClassId.Equals(classId)).ToList();
            }
            catch
            {
                throw;
            }
        }

        public List<Student> GetStudentsWithTeacherId(int teacherId)
        {
            try
            {
                return _dbContext.Student.Where(x => x.studentTeacherId.Equals(teacherId)).ToList();
            }
            catch
            {
                throw;
            }
        }

        public Student GetStudentDetails(int id)
        {
            try
            {
                Student? student = _dbContext.Student.Find(id);
                if (student != null)
                {
                    return student;
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

        public void AddStudent(Student student)
        {
            try
            {
                _dbContext.Student.Add(student);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                _dbContext.Entry(student).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public Student DeleteStudent(int id)
        {
            try
            {
                Student? student = _dbContext.Student.Find(id);

                if (student != null)
                {
                    _dbContext.Student.Remove(student);
                    _dbContext.SaveChanges();
                    return student;
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

        public bool CheckStudent(int id)
        {
            return _dbContext.Student.Any(e => e.studentId == id);
        }
    }
}
