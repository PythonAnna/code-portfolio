using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Application.Dtos;


namespace TestingPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroupRepository groupRepository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await groupRepository.GetAllAsync();
            return Ok(groups);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            var group = await groupRepository.GetByIdAsync(id);
            return Ok(group);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] GroupDto group)
        {
            var id = await groupRepository.CreateAsync(group);
            return StatusCode(StatusCodes.Status201Created, new { Id = id });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroupAsync([FromBody] GroupDto group)
        {

            await groupRepository.UpdateAsync(group);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            await groupRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
