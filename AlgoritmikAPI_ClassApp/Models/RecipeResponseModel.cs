namespace AlgoritmikAPI_ClassApp.Models
{
    public class RecipeResponseModel : ResponseModel
    {

        public List<RecipeModel> models { get; set; }

        public RecipeResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<RecipeModel>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
