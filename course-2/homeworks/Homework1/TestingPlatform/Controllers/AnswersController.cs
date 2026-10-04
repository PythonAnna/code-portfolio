using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllAnswers()
        {
            return Ok("Список вопросов...");
        }

        [HttpGet ("{id}")]
        public IActionResult GetAnswersById([FromRoute] int id) {
            if (id <= 0) return BadRequest("Неверный id");
            if (id == 1) return Ok("Ответ 1");
            return NotFound("Ответ не найден");
        }

        [HttpPost]
        public IActionResult CreateAnswer()
        {
            return Created("/api/answers/1", "Создан ответ 1");
        }

        [HttpDelete("{id}")]
        public IActionResult DaleteAnswer([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Неверный id");
            if (id != 1) return NotFound("Ответ не найден");
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult PutAnswer([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Неверный id");
            if (id != 1) return NotFound("Ответ не найден");
            return NoContent();
        }

    }
}
