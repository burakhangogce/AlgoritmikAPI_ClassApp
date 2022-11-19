using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IMyClass
    {
        public List<MyClass> GetMyClassDetails();
        public MyClass GetMyClassDetails(int id);
        public void AddMyClass(MyClass myClass);
        public void UpdateMyClass(MyClass myClass);
        public MyClass DeleteMyClass(int id);
        public bool CheckMyClass(int id);

    }
}
