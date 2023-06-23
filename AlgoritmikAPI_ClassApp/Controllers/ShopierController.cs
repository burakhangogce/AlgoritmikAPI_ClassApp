using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Route("api/shopier")]
    [ApiController]
    public class ShopierController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly INutritionist _INutritionist;

        public ShopierController(IConfiguration config, INutritionist INutritionist)
        {
            _configuration = config;
            _INutritionist = INutritionist;

        }



        [HttpGet("getorders")]
        public async Task<ActionResult<ResponseModel<List<NutritionistModel>>>> GetUnfilledOrders()
        {
            var response = new ResponseModel<List<NutritionistModel>>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                var responseString = ApiCall.GetApi("https://api.shopier.com/v1/orders?limit=10&page=1&sort=dateDesc");
                List<ShopierOrderModel>? orderList = JsonConvert.DeserializeObject<List<ShopierOrderModel>?>(responseString);
                List<NutritionistModel> filledNutritionist = new List<NutritionistModel>();
                if (orderList != null)
                {
                    List<ShopierOrderModel> unfilledOrders = orderList.Where(d => d.Status == "unfulfilled").ToList();
                    if (unfilledOrders.Count > 0)
                    {
                        for (int i = 0; i < unfilledOrders.Count; i++)
                        {
                            var order = unfilledOrders[i];
                            if (order.Note != null && order.Note != "")
                            {
                                int nutritionNo = int.Parse(order.Note);
                                NutritionistModel? nutritionistModel = await Task.FromResult(_INutritionist.GetNutritionist(nutritionNo));
                                if (nutritionistModel != null)
                                {
                                    nutritionistModel.fitimCoin += 10;
                                    _INutritionist.UpdateNutritionist(nutritionistModel);

                                    NutritionistModel model = await Task.FromResult(_INutritionist.GetNutritionist(nutritionNo));
                                    filledNutritionist.Add(model);
                                }
                            }
                        }
                    }
                }
                response.body = filledNutritionist;
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: "Genel bir hata oluştu.");
                return response;
            }
            return response;
        }


    }

    public static class ApiCall
    {
        public static string GetApi(string ApiUrl)
        {
            var responseString = "";
            var request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI3ZTBhZTI1NWFkMTFiNmQ2NzY3OTg3YTM1MzEwNTA4YSIsImp0aSI6IjM0M2Y2ZmUzN2I0NmNjYzA2N2NiY2YzYjNiYjJhNTU4OTNjZjEwM2MwNTA2ZjlkODgyZjE0NTYxZTgwY2JiYmU4ZDc3Y2JhNDYyYmYwMTM0NDlkZWZiMTUyYzZmYzA4NTFkYWM3ODczZGE3ZjBmYjdjZTkyYWMwZmMxY2NiMTY0MWIwZGRkMjliMTM5MzY5NWI0MjcyZjczOTc1MDM5NDYiLCJpYXQiOjE2Nzk5MTM0NjYsIm5iZiI6MTY3OTkxMzQ2NiwiZXhwIjoxODM3Njk4MjI2LCJzdWIiOiIzMDY5MTQiLCJzY29wZXMiOlsib3JkZXJzOnJlYWQiLCJvcmRlcnM6d3JpdGUiLCJwcm9kdWN0czpyZWFkIiwicHJvZHVjdHM6d3JpdGUiLCJzaGlwcGluZ3M6cmVhZCIsInNoaXBwaW5nczp3cml0ZSIsImRpc2NvdW50czpyZWFkIiwiZGlzY291bnRzOndyaXRlIiwicGF5b3V0czpyZWFkIiwicmVmdW5kczpyZWFkIiwicmVmdW5kczp3cml0ZSIsInNob3A6cmVhZCIsInNob3A6d3JpdGUiXX0.Jk9AbRQsq96sfI7s8-X0CaxHxQ21CtAztv7lUFrLKTPg-9F4vJN4Q_XvdK1nYrT4tjCK_HEUFNep1sac9jxOSpgrU3vpUe0Ur9Y2CWdR94FFl0Mtm3zLCv8ongam973K7A6C1zvZAPG-gCgndYcW64R1M-SA9lnDKBlHtWYMI3KQ_bxc9CZpeMu0A46hH3yNXPm-tQoE3eNDomZsdGc10q9beg504VzVoyMvSDwla-EuA4UTZ7GWy7uTknGtSX3j5r8rBJxONl6j00SvZ2D23wrJ2e6cRSw2STW0_L1bpaeFrfi4rWTr3Y5L5obBg1bV9neIO_1gd6ickfC_C0aEvA");
            request.Method = "GET";
            request.ContentType = "application/json";

            using (var response1 = request.GetResponse())
            {
                using (var reader = new StreamReader(response1.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
            }
            return responseString;
        }

        public static string PostApi(string ApiUrl, string postData = "")
        {

            var request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            var data = Encoding.ASCII.GetBytes(postData);
            request.Headers.Add("accept", "application/json");
            request.Headers.Add("authorization", "Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiI3ZTBhZTI1NWFkMTFiNmQ2NzY3OTg3YTM1MzEwNTA4YSIsImp0aSI6IjM0M2Y2ZmUzN2I0NmNjYzA2N2NiY2YzYjNiYjJhNTU4OTNjZjEwM2MwNTA2ZjlkODgyZjE0NTYxZTgwY2JiYmU4ZDc3Y2JhNDYyYmYwMTM0NDlkZWZiMTUyYzZmYzA4NTFkYWM3ODczZGE3ZjBmYjdjZTkyYWMwZmMxY2NiMTY0MWIwZGRkMjliMTM5MzY5NWI0MjcyZjczOTc1MDM5NDYiLCJpYXQiOjE2Nzk5MTM0NjYsIm5iZiI6MTY3OTkxMzQ2NiwiZXhwIjoxODM3Njk4MjI2LCJzdWIiOiIzMDY5MTQiLCJzY29wZXMiOlsib3JkZXJzOnJlYWQiLCJvcmRlcnM6d3JpdGUiLCJwcm9kdWN0czpyZWFkIiwicHJvZHVjdHM6d3JpdGUiLCJzaGlwcGluZ3M6cmVhZCIsInNoaXBwaW5nczp3cml0ZSIsImRpc2NvdW50czpyZWFkIiwiZGlzY291bnRzOndyaXRlIiwicGF5b3V0czpyZWFkIiwicmVmdW5kczpyZWFkIiwicmVmdW5kczp3cml0ZSIsInNob3A6cmVhZCIsInNob3A6d3JpdGUiXX0.Jk9AbRQsq96sfI7s8-X0CaxHxQ21CtAztv7lUFrLKTPg-9F4vJN4Q_XvdK1nYrT4tjCK_HEUFNep1sac9jxOSpgrU3vpUe0Ur9Y2CWdR94FFl0Mtm3zLCv8ongam973K7A6C1zvZAPG-gCgndYcW64R1M-SA9lnDKBlHtWYMI3KQ_bxc9CZpeMu0A46hH3yNXPm-tQoE3eNDomZsdGc10q9beg504VzVoyMvSDwla-EuA4UTZ7GWy7uTknGtSX3j5r8rBJxONl6j00SvZ2D23wrJ2e6cRSw2STW0_L1bpaeFrfi4rWTr3Y5L5obBg1bV9neIO_1gd6ickfC_C0aEvA");
            request.Method = "POST";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }
    }
}
