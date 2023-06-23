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
        public async Task<ActionResult<ResponseModel<DietModel>>> Get(int id)
        {
            var response = new ResponseModel<DietModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                DietModel diet = await Task.FromResult(_IDiet.GetDiet(id));
                if (diet == null)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyet bulunamadı.");
                    return response;
                }
                response.body = diet;
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
        public async Task<ActionResult<ResponseModel<IEnumerable<DietModel>>>> GetClientDiets(int id)
        {
            var response = new ResponseModel<IEnumerable<DietModel>>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                List<DietModel> diets = await Task.FromResult(_IDiet.GetClientDiets(id));
                if (diets == null || diets.Count <= 0)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyet bulunamadı.");
                    return response;
                }
                response.body = diets;
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
        public async Task<ActionResult<ResponseModel<IEnumerable<DietModel>>>> GetNutritionDiets(int id)
        {
            var response = new ResponseModel<IEnumerable<DietModel>>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                List<DietModel> diets = await Task.FromResult(_IDiet.GetNutritionDiets(id));
                if (diets == null || diets.Count <= 0)
                {
                    response.isSuccess = false;
                    response.errorModel = new ErrorResponseModel(errorMessage: "Diyet bulunamadı.");
                    return response;
                }
                response.body = diets;
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
        public async Task<ActionResult<ResponseModel<DietModel>>> Post(DietModel diet)
        {
            var response = new ResponseModel<DietModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                _IDiet.AddDiet(diet);
            }
            catch (Exception ex)
            {
                response.statusCode = 400;
                response.isSuccess = false;
                response.errorModel = new ErrorResponseModel(errorMessage: ex.Message);
                return response;
            }
            response.body = await Task.FromResult(diet);

            return response;
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<ResponseModel<DietModel>>> Put(int id, DietModel dietModel)
        {
            var response = new ResponseModel<DietModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
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
                    response.body = await Task.FromResult(dietModel);
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
        public async Task<ActionResult<ResponseModel<DietModel>>> Delete(int id)
        {
            var response = new ResponseModel<DietModel>(isSuccess: true, statusCode: 200, body: null, errorModel: null);
            try
            {
                var diet = _IDiet.DeleteDiet(id);
                response.body = await Task.FromResult(diet);
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
