using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/info/performance")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformance _IPerformance;

        public PerformanceController(IPerformance IPerformance)
        {
            _IPerformance = IPerformance;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Performance>>> Get()
        {
            return await Task.FromResult(_IPerformance.GetPerformanceDetails());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Performance>> Get(int id)
        {
            var performance = await Task.FromResult(_IPerformance.GetPerformanceDetails(id));
            if (performance == null)
            {
                return NotFound();
            }
            return performance;
        }

        [HttpPost]
        public async Task<ActionResult<Performance>> Post(Performance performance)
        {
            _IPerformance.AddPerformance(performance);
            return await Task.FromResult(performance);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Performance>> Put(int id, Performance performance)
        {
            if (id != performance.performanceId)
            {
                return BadRequest();
            }
            try
            {
                _IPerformance.UpdatePerformance(performance);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(performance);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Performance>> Delete(int id)
        {
            var performance = _IPerformance.DeletePerformance(id);
            return await Task.FromResult(performance);
        }

        private bool PerformanceExists(int id)
        {
            return _IPerformance.CheckPerformance(id);
        }
    }
}
