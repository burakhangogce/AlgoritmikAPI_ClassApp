using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Route("api/invite")]
    [ApiController]
    public class InviteController : ControllerBase
    {
        private readonly IInvite _IInvite;

        public InviteController(IInvite IInvite)
        {
            _IInvite = IInvite;
        }

        [HttpGet("getInvite/{id}")]
        public async Task<ActionResult<InviteResponseModel>> GetInvite(string id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new InviteResponseModel(responseModel: responseModel);
            try
            {
                InviteModel client = await Task.FromResult(_IInvite.GetInvite(id));
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

        [HttpGet("getInvites/{id}")]
        public async Task<ActionResult<InviteResponseModel>> GetInvites(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new InviteResponseModel(responseModel: responseModel);
            try
            {
                List<InviteModel> invites = await Task.FromResult(_IInvite.GetInvites(id));
                response.models.AddRange(invites);
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

        [HttpPost("addInvite")]
        public async Task<ActionResult<InviteResponseModel>> Post(InviteModel inviteModel)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new InviteResponseModel(responseModel: responseModel);
            try
            {
                _IInvite.AddInvite(inviteModel);
                response.models.Add(inviteModel);
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
    }
}
