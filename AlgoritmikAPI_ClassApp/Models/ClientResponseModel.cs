namespace AlgoritmikAPI_ClassApp.Models
{
    public class ClientResponseModel : ResponseModel
    {

        public List<ClientModel> models { get; set; }

        public ClientResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<ClientModel>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
