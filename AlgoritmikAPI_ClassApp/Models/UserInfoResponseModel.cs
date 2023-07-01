using Firebase.Auth;

namespace AlgoritmikAPI_ClassApp.Models
{
    public class UserInfoResponseModel : ResponseModel
    {

        public List<FirebaseAuthLink> models { get; set; }

        public UserInfoResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<FirebaseAuthLink>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
