using AlgoritmikAPI_ClassApp.Interface;
using AlgoritmikAPI_ClassApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlgoritmikAPI_ClassApp.Controllers
{
    [Authorize]
    [Route("api/info/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudent _IStudent;

        public StudentController(IStudent IStudent)
        {
            _IStudent = IStudent;
        }

        [HttpGet("teacher/{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsWithTeacherId(int id)
        {

            List<Student> students = await Task.FromResult(_IStudent.GetStudentsWithTeacherId(id));
            if (students.Count == 0)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var students = await Task.FromResult(_IStudent.GetStudentDetails(id));
            if (students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet("class/{id}")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsWithClassId(int id)
        {
            List<Student> students = await Task.FromResult(_IStudent.GetStudentsWithClassId(id));
            if (students.Count == 0)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> Post(Student student)
        {
            _IStudent.AddStudent(student);
            return await Task.FromResult(student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Student>> Put(int id, Student student)
        {
            if (id != student.studentId)
            {
                return BadRequest();
            }
            try
            {
                _IStudent.UpdateStudent(student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return await Task.FromResult(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Student>> Delete(int id)
        {
            var student = _IStudent.DeleteStudent(id);
            return await Task.FromResult(student);
        }

        private bool StudentExists(int id)
        {
            return _IStudent.CheckStudent(id);
        }
    }
}
