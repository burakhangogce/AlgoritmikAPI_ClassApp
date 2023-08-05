using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/nutritionist")]
    [ApiController]
    public class NutritionistController : ControllerBase
    {
        private readonly INutritionist _INutritionist;

        public NutritionistController(INutritionist INutritionist)
        {
            _INutritionist = INutritionist;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NutritionistResponseModel>> Get(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new NutritionistResponseModel(responseModel: responseModel);
            try
            {
                NutritionistModel nutritionist = await Task.FromResult(_INutritionist.GetNutritionist(id));
                if (nutritionist == null)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyetisyen bulunamadı.");
                    return response;
                }
                else
                {
                    response.models.Add(nutritionist);
                }
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


        [HttpGet("getNutritionistWithUserId/{id}")]
        public async Task<ActionResult<NutritionistResponseModel>> GetNutritionistWithUserId(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new NutritionistResponseModel(responseModel: responseModel);
            try
            {
                NutritionistModel nutritionist = await Task.FromResult(_INutritionist.GetNutritionistWithUserId(id));
                response.models.Add(nutritionist);
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



        [HttpPut("update/{id}")]
        public async Task<ActionResult<NutritionistResponseModel>> Put(int id, NutritionistModel nutritionist)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new NutritionistResponseModel(responseModel: responseModel);
            try
            {
                if (id != nutritionist.nutritionistId)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyetisyen bulunamadı!");
                    return response;
                }
                try
                {
                    _INutritionist.UpdateNutritionist(nutritionist);
                    response.models.Add(nutritionist);
                    return response;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!NutritionistExists(id))
                    {
                        response.isSuccess = false;
                        response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                        return response;
                    }
                    else
                    {
                        response.errorModel = new ErrorResponseModel(errorMessage: "Diyetisyen bulunamadı!");
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

        private bool NutritionistExists(int id)
        {
            return _INutritionist.CheckNutritionist(id);
        }
    }
}
