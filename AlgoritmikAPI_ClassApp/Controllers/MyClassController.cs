using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/info/myclass")]
    [ApiController]
    public class MyClassController : ControllerBase
    {
        private readonly IMyClass _IMyClass;

        public MyClassController(IMyClass IMyClass)
        {
            _IMyClass = IMyClass;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MyClass>>> Get()
        {
            return await Task.FromResult(_IMyClass.GetMyClassDetails());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MyClass>> Get(int id)
        {
            var myclass = await Task.FromResult(_IMyClass.GetMyClassDetails(id));
            if (myclass == null)
            {
                return NotFound();
            }
            return myclass;
        }

        [HttpPost]
        public async Task<ActionResult<MyClass>> Post(MyClass myClass)
        {
            _IMyClass.AddMyClass(myClass);
            return await Task.FromResult(myClass);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MyClass>> Put(int id, MyClass myClass)
        {
            if (id != myClass.classId)
            {
                return BadRequest();
            }
            try
            {
                _IMyClass.UpdateMyClass(myClass);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MyClassExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(myClass);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MyClass>> Delete(int id)
        {
            var myClass = _IMyClass.DeleteMyClass(id);
            return await Task.FromResult(myClass);
        }

        private bool MyClassExists(int id)
        {
            return _IMyClass.CheckMyClass(id);
        }
    }
}
