using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/diet/mydiets")]
    [ApiController]
    public class DietController : ControllerBase
    {
        private readonly IDiet _IDiet;

        public DietController(IDiet IDiet)
        {
            _IDiet = IDiet;
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<DietResponseModel>> Get(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new DietResponseModel(responseModel: responseModel);
            try
            {
                DietModel diet = await Task.FromResult(_IDiet.GetDiet(id));
                if (diet == null)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyet bulunamadı.");
                    return response;
                }
                else
                {
                    response.models.Add(diet);
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
        [HttpGet("getclientdiets/{id}")]
        public async Task<ActionResult<DietResponseModel>> GetClientDiets(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new DietResponseModel(responseModel: responseModel);
            try
            {
                List<DietModel> diets = await Task.FromResult(_IDiet.GetClientDiets(id));
                if (diets == null || diets.Count <= 0)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyet bulunamadı.");
                    return response;
                }
                else
                {
                    response.models.AddRange(diets);
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
        [HttpGet("getnutritiondiets/{id}")]
        public async Task<ActionResult<DietResponseModel>> GetNutritionDiets(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new DietResponseModel(responseModel: responseModel);
            try
            {
                List<DietModel> diets = await Task.FromResult(_IDiet.GetNutritionDiets(id));
                if (diets == null || diets.Count <= 0)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyet bulunamadı.");
                    return response;
                }
                else
                {
                    response.models.AddRange(diets);
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

        [HttpPost("adddiet")]
        public async Task<ActionResult<DietResponseModel>> Post(DietModel diet)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new DietResponseModel(responseModel: responseModel);
            try
            {
                _IDiet.AddDiet(diet);
                response.models.Add(diet);
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
        public async Task<ActionResult<DietResponseModel>> Put(int id, DietModel dietModel)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new DietResponseModel(responseModel: responseModel);
            try
            {
                if (id != dietModel.dietId)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Client bulunamadı!");
                    return response;
                }
                try
                {
                    _IDiet.UpdateDiet(dietModel);
                    response.models.Add(dietModel);
                    return response;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    response.statusCode = 400;
                    if (!DietExists(id))
                    {
                        response.isSuccess = false;
                        response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                        return response;
                    }
                    else
                    {
                        response.errorModel = new ErrorResponseModel(errorMessage: "Diyet bulunamadı!");
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

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<DietResponseModel>> Delete(int id)
        {
            ResponseModel responseModel = new ResponseModel(isSuccess: true, statusCode: 200, errorModel: null);
            var response = new DietResponseModel(responseModel: responseModel);
            try
            {
                var diet = _IDiet.DeleteDiet(id);
                response.models.Add(diet);
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

        private bool DietExists(int id)
        {
            return _IDiet.CheckDiet(id);
        }
    }
}
