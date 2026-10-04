using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllQuestions()
        {
            return Ok("Список вопросов...");
        }

        [HttpGet("{id}")]
        public IActionResult GetQuestionById([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");
            if (id == 1) return Ok("Вопрос 1");
            return NotFound("Вопрос не найден");
        }

        [HttpPost]
        public IActionResult CreateQuestion()
        {
            return Created("/api/questions/1","Вопрос 1 создан!");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateQuestion([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");
            if (id != 1) return NotFound("Вопрос не найден");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuestion([FromRoute] int id)
        {
            if (id <= 0) return BadRequest("Некоректный id");
            if (id != 0) return NotFound("Вопрос не найден");
            return NoContent();
        }
    }
}
