namespace AlgoritmikAPI_ClassApp.Models
{
    public class VersionResponseModel : ResponseModel
    {

        public List<VersionModel> models { get; set; }

        public VersionResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<VersionModel>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
