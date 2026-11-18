using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Application.Dtos;
using AutoMapper;
using TestingPlatform.API.Requests.Group;
using TestingPlatform.API.Responses.Group;
using TestingPlatform.Infrastructure.Repositories;


namespace TestingPlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController(IGroupRepository groupRepository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await groupRepository.GetAllAsync();
            return Ok(mapper.Map<IEnumerable<GroupResponse>>(groups));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            var group = await groupRepository.GetByIdAsync(id);
            return Ok(mapper.Map<GroupResponse>(group));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest group)
        {
            var groupDto = new GroupDto()
            {
                Name = group.Name,
                DirectionId = group.DirectionId,
                ProjectId = group.ProjectId,
                CourseId = group.CourseId
            };

            var groupId = await groupRepository.CreateAsync(groupDto);
            return StatusCode(StatusCodes.Status201Created, new { Id = groupId });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroupAsync([FromBody] UpdateGroupRequest group)
        {
            await groupRepository.UpdateAsync(mapper.Map<GroupDto>(group));
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
