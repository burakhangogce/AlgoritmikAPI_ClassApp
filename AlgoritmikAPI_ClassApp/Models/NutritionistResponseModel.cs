namespace AlgoritmikAPI_ClassApp.Models
{
    public class NutritionistResponseModel : ResponseModel
    {

        public List<NutritionistModel> models { get; set; }

        public NutritionistResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<NutritionistModel>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
