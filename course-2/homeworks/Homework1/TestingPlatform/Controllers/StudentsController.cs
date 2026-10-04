using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            return Ok("Сисок студентов...");
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentsById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");

            if (id == 1) return Ok("Студент 1");

            return NotFound("Студент с id = "+id+" не найден");
        }

        [HttpPost]
        public IActionResult CreateStudent()
        {
            return Created("/api/students/1", "Студент 1 создан!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");
            if (id != 1) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");
            if (id != 1) return NotFound();
            return NoContent();
        }
    }
}
