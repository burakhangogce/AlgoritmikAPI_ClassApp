namespace AlgoritmikAPI_ClassApp.Models
{
    public class ShopierOrderResponseModel : ResponseModel
    {

        public List<ShopierOrderModel> models { get; set; }

        public ShopierOrderResponseModel(ResponseModel responseModel) : base(responseModel.isSuccess, responseModel.statusCode, responseModel.errorModel)
        {
            this.models = new List<ShopierOrderModel>();
            this.isSuccess = responseModel.isSuccess;
            this.statusCode = responseModel.statusCode;
            this.errorModel = responseModel.errorModel;
        }
    }
}
