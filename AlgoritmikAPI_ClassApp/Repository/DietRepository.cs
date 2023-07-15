using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;

namespace AlgoritmikAPI_ClassApp.Repository
{
    public class DietRepository : IDiet
    {
        readonly DatabaseContext _dbContext = new();

        public DietRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }


        public DietModel GetDiet(int id)
        {
            try
            {
                DietModel? dietModel = _dbContext.Diets!.Find(id);
                if (dietModel != null)
                {
                    List<DietDayModel> dietDayList = _dbContext.DietDays!.Where(x => x.dietId.Equals(id)).ToList();
                    if (dietDayList != null)
                    {
                        for (int i = 0; i < dietDayList.Count; i++)
                        {
                            List<DietMenuModel> dietDayMenuList = _dbContext.DietDayMenus!.Where(x => x.dietDayId.Equals(dietDayList[i].dietDayId)).ToList();
                            dietDayList[i].dietMenus = dietDayMenuList;
                        }
                    }
                    dietModel.dietDayModel = dietDayList!;
                    return dietModel;
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

        public List<DietModel> GetClientDiets(int id)
        {
            try
            {
                List<DietModel> dietList = _dbContext.Diets!.Where(x => x.clientId.Equals(id)).ToList();
                for (int y = 0; y < dietList.Count; y++)
                {
                    DietModel dietModel = dietList[y];
                    List<DietDayModel> dietDayList = _dbContext.DietDays!.Where(x => x.dietId.Equals(dietList[y].dietId)).ToList();
                    if (dietDayList != null)
                    {
                        for (int i = 0; i < dietDayList.Count; i++)
                        {
                            List<DietMenuModel> dietDayMenuList = _dbContext.DietDayMenus!.Where(x => x.dietDayId.Equals(dietDayList[i].dietDayId)).ToList();
                            dietDayList[i].dietMenus = dietDayMenuList;
                        }
                    }
                    dietModel.dietDayModel = dietDayList!;
                }
                return dietList;
            }
            catch
            {
                throw;
            }
        }

        public List<DietModel> GetNutritionDiets(int id)
        {
            try
            {
                List<DietModel> dietList = _dbContext.Diets!.Where(x => x.nutritionistId.Equals(id)).ToList();
                for (int y = 0; y < dietList.Count; y++)
                {
                    DietModel dietModel = dietList[y];
                    List<DietDayModel> dietDayList = _dbContext.DietDays!.Where(x => x.dietId.Equals(dietList[y].dietId)).ToList();
                    if (dietDayList != null)
                    {
                        for (int i = 0; i < dietDayList.Count; i++)
                        {
                            List<DietMenuModel> dietDayMenuList = _dbContext.DietDayMenus!.Where(x => x.dietDayId.Equals(dietDayList[i].dietDayId)).ToList();
                            dietDayList[i].dietMenus = dietDayMenuList;
                        }
                    }
                    dietModel.dietDayModel = dietDayList!;
                }
                return dietList;
            }
            catch
            {
                throw;
            }
        }


        public void AddDiet(DietModel dietModel)
        {
            try
            {
                _dbContext.Diets!.Add(dietModel);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateDiet(DietModel currentDiet)
        {
            try
            {
                List<DietMenuModel> deleteMenus = new List<DietMenuModel>();

                DietModel dbDiet = _dbContext.Diets!.First(x => x.dietId == currentDiet.dietId);
                foreach (var currentDietDay in dbDiet.dietDayModel)
                {
                    DietDayModel? dbDietDayModel = dbDiet.dietDayModel.FirstOrDefault(y => y.dietDayId == currentDietDay.dietDayId);
                    if (dbDietDayModel != null)
                    {
                        foreach (var menus in dbDietDayModel.dietMenus)
                        {
                            if (currentDietDay.dietMenus.FirstOrDefault(z => z.dietMenuId == menus.dietMenuId) == null)
                            {
                                _dbContext.DietDayMenus.Remove(menus);
                            };
                        }
                    }
                }
                _dbContext.Diets!.UpdateRange(currentDiet);
                _dbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public DietModel DeleteDiet(int id)
        {
            try
            {
                DietModel? dietModel = _dbContext.Diets.Find(id);

                if (dietModel != null)
                {
                    _dbContext.Diets.Remove(dietModel);
                    _dbContext.SaveChanges();
                    return dietModel;
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

        public bool CheckDiet(int id)
        {
            return _dbContext.Diets.Any(e => e.dietId == id);
        }


    }
}
