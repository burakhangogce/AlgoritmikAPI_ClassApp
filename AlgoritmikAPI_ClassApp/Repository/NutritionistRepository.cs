using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class NutritionistRepository : INutritionist
    {
        readonly DatabaseContext _dbContext = new();

        public NutritionistRepository(DatabaseContext dbContext)
        { 
            _dbContext = dbContext;
        }

        public NutritionistModel GetNutritionist(int id)
        {
            try
            {
                NutritionistModel? nutritionistModel = _dbContext.Nutritionist!.Find(id);
                if (nutritionistModel != null)
                {

                    return nutritionistModel;
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

        public void UpdateNutritionist(NutritionistModel nutritionistModel)
        {
            try
            {
                _dbContext.Nutritionist!.Update(nutritionistModel);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

     

        public bool CheckNutritionist(int id)
        {
            return _dbContext.Nutritionist.Any(e => e.nutritionistId == id);
        }

       
    }
}
