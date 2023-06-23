using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _IClient;

        public ClientController(IClient IClient)
        {
            _IClient = IClient;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<ClientModel>>> Get(int id)
        {
            var response = new ResponseModel<ClientModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                ClientModel client = await Task.FromResult(_IClient.GetClient(id));
                response.body = client;
                return response;
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }

        }


        [HttpGet("myclients/{id}")]
        public async Task<ActionResult<ResponseModel<IEnumerable<ClientModel>>>> GetMyClients(int id)
        {
            var response = new ResponseModel<IEnumerable<ClientModel>>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                List<ClientModel> myClients = await Task.FromResult(_IClient.GetMyClients(id));
                response.body = myClients;
                return response;
            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.statusCode = 400;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }

        }

        [HttpPost("addclient")]
        public async Task<ActionResult<ResponseModel<ClientModel>>> Post(ClientModel client)
        {
            var response = new ResponseModel<ClientModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                _IClient.AddClient(client);
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }
            response.body = await Task.FromResult(client);

            return response;
        }




        [HttpPut("update/{id}")]
        public async Task<ActionResult<ResponseModel<ClientModel>>> Put(int id, ClientModel clientModel)
        {
            var response = new ResponseModel<ClientModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                if (id != clientModel.clientId)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Client bulunamadı!");
                    return response;
                }
                try
                {
                    _IClient.UpdateClient(clientModel);
                    response.body = await Task.FromResult(clientModel);
                    return response;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    response.statusCode = 400;
                    if (!ClientExists(id))
                    {
                        response.isSuccess = false;
                        response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                        return response;
                    }
                    else
                    {
                        response.errorModel = new ErrorResponseModel(errorMessage: "Client bulunamadı!");
                        return response;
                    }
                }

            }
            catch (Exception ex)
            {
                response.isSuccess = false;
                response.statusCode = 400;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }

        }

        private bool ClientExists(int id)
        {
            return _IClient.CheckClient(id);
        }
    }
}
