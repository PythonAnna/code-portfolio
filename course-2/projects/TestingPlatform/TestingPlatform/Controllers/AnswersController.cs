using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestingPlatform.API.Requests.Answer;
using TestingPlatform.API.Responses;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController(IAnswerRepository answerRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAnswers()
        {
            var answer = await answerRepository.GetAllAnswers();
            return Ok(mapper.Map<IEnumerable<AnswerResponse>>(answer));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswersById([FromRoute] int id)
        {
            var answer = await answerRepository.GetAnswersById(id);
            return Ok(mapper.Map<AnswerResponse>(answer));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerRequest answer)
        {
            var answerDto = new AnswerDto()
            {
                Text = answer.Text,
                IsCorrect = answer.IsCorrect
            };
            var answerId = await answerRepository.CreateAnswer(answerDto);
            return StatusCode(StatusCodes.Status201Created, new { Id = answerId });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAnswer([FromBody] UpdateAnswerRequest answer)
        {
            await answerRepository.UpdateAnswer(mapper.Map<AnswerDto>(answer));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] int id)
        {
            await answerRepository.DeleteAnswer(id);
            return NoContent();
        }
    }
}
