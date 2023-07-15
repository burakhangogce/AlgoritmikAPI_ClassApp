using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Route("api/version")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        private readonly IVersion _IVersion;

        public VersionController(IVersion IVersion)
        {
            _IVersion = IVersion;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<VersionResponseModel>> Get(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new VersionResponseModel(responseModel: responseModel);
            try
            {
                VersionModel version = await Task.FromResult(_IVersion.GetVersion(id));
                response.models.Add(version);
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


        [HttpGet("versions")]
        public async Task<ActionResult<VersionResponseModel>> GetVersions()
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new VersionResponseModel(responseModel: responseModel);
            try
            {
                List<VersionModel> versions = await Task.FromResult(_IVersion.GetVersions());
                response.models.AddRange(versions);
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
    }
}
