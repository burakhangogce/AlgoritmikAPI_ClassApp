namespace AlgoritmikAPI_ClassApp.Models
{
    public class InviteResponseModel : ResponseModel
    {

        public List<InviteModel> models { get; set; }

        public InviteResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<InviteModel>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
