using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Interface
{
    public interface INutritionist
    {
        public NutritionistModel GetNutritionist(int id);
        public void UpdateNutritionist(NutritionistModel nutritionistModel);
        public bool CheckNutritionist(int id);

    }
}
