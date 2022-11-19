using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IStudent
    {
        public List<Student> GetStudentDetails();
        public List<Student> GetStudentsWithClassId(int id);
        public List<Student> GetStudentsWithTeacherId(int id);
        public Student GetStudentDetails(int id);
        public void AddStudent(Student student);
        public void UpdateStudent(Student student);
        public Student DeleteStudent(int id);
        public bool CheckStudent(int id);
    }
}
