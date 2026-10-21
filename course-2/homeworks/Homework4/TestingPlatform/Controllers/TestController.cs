using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllTests()
        {
            return Ok("Список тестов...");
        }

        [HttpGet("{id}")]
        public IActionResult GetTestById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");
            if (id == 1) return Ok("Тест 1");
            return NotFound("Тест не найден");
        }

        [HttpPost]
        public IActionResult CreateTest()
        {
            return Created("/api/tests/1", "Тест 1 создан!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTest([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");
            if (id != 1) return NotFound("Тест не найден");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTest([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");
            if (id != 1) return NotFound("Тест не найден");
            return NoContent();
        }
    }
}
