using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public async Task<ActionResult<DietModel>> Get(int id)
        {
            DietModel diet = await Task.FromResult(_IDiet.GetDiet(id));

            if (diet == null)
            {
                return NotFound();
            }
            return diet;
        }

        [HttpPost]
        public async Task<ActionResult<DietModel>> Post(DietModel diet)
        {
            _IDiet.AddDiet(diet);
            return await Task.FromResult(diet);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<DietModel>> Put(int id, DietModel dietModel)
        {
            if (id != dietModel.dietId)
            {
                return BadRequest();
            }
            try
            {
                _IDiet.UpdateDiet(dietModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DietExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(dietModel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DietModel>> Delete(int id)
        {
            var diet = _IDiet.DeleteDiet(id);
            return await Task.FromResult(diet);
        }

        private bool DietExists(int id)
        {
            return _IDiet.CheckDiet(id);
        }
    }
}
