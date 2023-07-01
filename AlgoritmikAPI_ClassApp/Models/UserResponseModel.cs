namespace AlgoritmikAPI_ClassApp.Models
{
    public class UserResponseModel : ResponseModel
    {

        public List<UserModel> models { get; set; }

        public UserResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<UserModel>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
