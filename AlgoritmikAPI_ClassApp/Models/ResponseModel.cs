namespace AlgoritmikAPI_ClassApp.Models
{
    public class ResponseModel<T>
    {

        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public ErrorResponseModel? errorModel { get; set; }
        public T? body { get; set; }

        public ResponseModel(bool isSuccess, int statusCode, ErrorResponseModel errorModel, T? body)
        {
            this.isSuccess = isSuccess;
            this.statusCode = statusCode;
            this.errorModel = errorModel;
            this.body = body;
        }


    }

    public class ErrorResponseModel
    {
        public string errorMessage { get; set; }
        public ErrorResponseModel(string errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
