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
        public async Task<ActionResult<ResponseModel<NutritionistModel>>> Get(int id)
        {
            var response = new ResponseModel<NutritionistModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                NutritionistModel nutritionist = await Task.FromResult(_INutritionist.GetNutritionist(id));
                if (nutritionist == null)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyetisyen bulunamadı.");
                    return response;
                }
                response.body = nutritionist;
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
        public async Task<ActionResult<ResponseModel<NutritionistModel>>> Put(int id, NutritionistModel nutritionist)
        {
            var response = new ResponseModel<NutritionistModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
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
                    response.body = await Task.FromResult(nutritionist);
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
