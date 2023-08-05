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
        public async Task<ActionResult<ClientResponseModel>> Get(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new ClientResponseModel(responseModel: responseModel);
            try
            {
                ClientModel client = await Task.FromResult(_IClient.GetClient(id));
                response.models.Add(client);
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


        [HttpGet("getClientWithUserId/{id}")]
        public async Task<ActionResult<ClientResponseModel>> GetClientWithUserId(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new ClientResponseModel(responseModel: responseModel);
            try
            {
                ClientModel client = await Task.FromResult(_IClient.GetClientWithUserId(id));
                response.models.Add(client);
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
        public async Task<ActionResult<ClientResponseModel>> GetMyClients(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new ClientResponseModel(responseModel: responseModel);
            try
            {
                List<ClientModel> myClients = await Task.FromResult(_IClient.GetMyClients(id));
                response.models.AddRange(myClients);
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
        public async Task<ActionResult<ClientResponseModel>> Post(ClientModel client)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new ClientResponseModel(responseModel: responseModel);
            try
            {
                _IClient.AddClient(client);
                response.models.Add(client);
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }
            return response;
        }




        [HttpPut("update/{id}")]
        public async Task<ActionResult<ClientResponseModel>> Put(int id, ClientModel clientModel)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new ClientResponseModel(responseModel: responseModel);
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
                    response.models.Add(clientModel);
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
