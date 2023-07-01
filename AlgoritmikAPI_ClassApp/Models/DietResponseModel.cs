namespace AlgoritmikAPI_ClassApp.Models
{
    public class DietResponseModel : ResponseModel
    {

        public List<DietModel> models { get; set; }


        public DietResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<DietModel>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }


    }
}
