using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<NutritionistModel>> Get(int id)
        {
            try
            {
                NutritionistModel nutritionist = await Task.FromResult(_INutritionist.GetNutritionist(id));

                if (nutritionist == null)
                {
                    return NotFound();
                }
                return nutritionist;
            }
            catch (Exception)
            {

                throw;
            }
            
        }

     

        [HttpPut("update/{id}")]
        public async Task<ActionResult<NutritionistModel>> Put(int id, NutritionistModel nutritionist)
        {
            if (id != nutritionist.nutritionistId)
            {
                return BadRequest();
            }
            try
            {
                _INutritionist.UpdateNutritionist(nutritionist);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NutritionistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(nutritionist);
        }

        private bool NutritionistExists(int id)
        {
            return _INutritionist.CheckNutritionist(id);
        }
    }
}
