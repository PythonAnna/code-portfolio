using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TestingPlatform.API.Requests.Test;
using TestingPlatform.API.Responses;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Repositories;
using TestingPlatform.Domain.Enum;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(ITestRepository testRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllTests([FromQuery] bool? IsPublic, [FromQuery] List<int> groupIds, [FromQuery] List<int> studentId)
        {
            var tests = await testRepository.GetAllTests(IsPublic, groupIds, studentId);
            return Ok(mapper.Map<IEnumerable<TestResponse>>(tests));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await testRepository.GetTestById(id);
            return Ok(mapper.Map<TestResponse>(test));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest([FromBody] CreateTestRequest test)
        {
            var testDto = new TestDto()
            {
                Title = test.Title,
                Description = test.Description,
                IsRepeatable = test.IsRepeatable,
                Type = test.Type,
                PublishedAt = test.PublishedAt,
                Deadline = test.Deadline,
                DurationMinutes = test.DurationMinutes,
                IsPublic = test.IsPublic,
                PassingScore = test.PassingScore,
                MaxAttempts = test.MaxAttempts
            };
            var testId = await testRepository.CreateTest(testDto);
            return StatusCode(StatusCodes.Status201Created, new { Id = testId });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTest([FromBody] UpdateTestRequest test)
        {
            await testRepository.UpdateTest(mapper.Map<TestDto>(test));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTest([FromRoute] int id)
        {
            await testRepository.DeleteTest(id);
            return NoContent();
        }

        [HttpGet("student/{id:int}")]
        public async Task<IActionResult> GetAllForStudentAsync([FromRoute] int id)
        {
            var forStudent = await testRepository.GetAllForStudentAsync(id);
            return Ok(mapper.Map<IEnumerable<TestResponse>>(forStudent));
        }

        [HttpGet("top/{count:int}")]
        public async Task<IActionResult> GetTopRecentAsync([FromRoute] int count)
        {
            var top = await testRepository.GetTopRecentAsync(count);
            return Ok(mapper.Map<IEnumerable<TestResponse>>(top));
        }

        [HttpGet("stats/count")]
        public async Task<IActionResult> GetTestCountByAsync()
        {
            var count = await testRepository.GetTestCountByAsync();
            return Ok(mapper.Map<IEnumerable<TestResponse>>(count));
        }

        [HttpGet("stats/courses")]
        public async Task<IActionResult> GetCourseStatsAsync()
        {
            var status = await testRepository.GetCourseStatsAsync();
            return Ok(mapper.Map<IEnumerable<TestResponse>>(status));
        }

        [HttpGet("stats/directions")]
        public async Task<IActionResult> GetDirectionAveragesAsync()
        {
            var directionAverages = await testRepository.GetDirectionAveragesAsync();
            return Ok(mapper.Map<IEnumerable<TestResponse>>(directionAverages));
        }

        [HttpGet("stats/timeline")]
        public async Task<IActionResult> GetTimelineByPublicAsync()
        {
            var timeline = await testRepository.GetTimelineByPublicAsync();
            return Ok(mapper.Map<IEnumerable<TestResponse>>(timeline));
        }

        [HttpGet("stats/top-groups/{top:int}")]
        public async Task<IActionResult> GetTopGroupsByTestCountAsync([FromRoute] int top)
        {
            var topGroups = await testRepository.GetTopGroupsByTestCountAsync(top);
            return Ok(mapper.Map<IEnumerable<TestResponse>>(topGroups));
        }
    }
}
