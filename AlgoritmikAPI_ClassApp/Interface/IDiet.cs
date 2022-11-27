using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface IDiet
    {
        public DietModel GetDiet(int id);
        public void AddDiet(DietModel dietModel);
        public void UpdateDiet(DietModel dietModel);
        public DietModel DeleteDiet(int id);
        public bool CheckDiet(int id);

    }
}
