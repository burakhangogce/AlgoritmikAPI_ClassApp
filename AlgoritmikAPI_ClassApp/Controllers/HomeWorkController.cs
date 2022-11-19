using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/info/homework")]
    [ApiController]
    public class HomeWorkController : ControllerBase
    {
        private readonly IHomeWork _IHomeWork;

        public HomeWorkController(IHomeWork IHomeWork)
        {
            _IHomeWork = IHomeWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HomeWork>>> Get()
        {
            return await Task.FromResult(_IHomeWork.GetHomeWorkDetails());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HomeWork>> Get(int id)
        {
            var homework = await Task.FromResult(_IHomeWork.GetHomeWorkDetails(id));
            if (homework == null)
            {
                return NotFound();
            }
            return homework;
        }

        [HttpPost]
        public async Task<ActionResult<HomeWork>> Post(HomeWork homework)
        {
            _IHomeWork.AddHomeWork(homework);
            return await Task.FromResult(homework);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HomeWork>> Put(int id, HomeWork homework)
        {
            if (id != homework.homeWorkId)
            {
                return BadRequest();
            }
            try
            {
                _IHomeWork.UpdateHomeWork(homework);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HomeWorkExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(homework);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<HomeWork>> Delete(int id)
        {
            var homework = _IHomeWork.DeleteHomeWork(id);
            return await Task.FromResult(homework);
        }

        private bool HomeWorkExists(int id)
        {
            return _IHomeWork.CheckHomeWork(id);
        }
    }
}
