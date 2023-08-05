namespace AlgoritmikAPI_ClassApp.Models
{
    public class UserInfoResponseModel : ResponseModel
    {

        public List<UserInfo> models { get; set; }

        public UserInfoResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<UserInfo>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
