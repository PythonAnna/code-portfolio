using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.Interfaces;
//using TestingPlatform.Data;
using TestingPlatform.Infrastructure.Repositories;
using TestingPlatform.Models;

namespace TestingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        public GroupsController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var groups = _groupRepository.GetAll();
            return Ok(groups);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetGroupById(int id)
        {
            var group = _groupRepository.GetById(id);
            return Ok(group);
        }

        [HttpPost]
        public IActionResult CreateGroup([FromBody] Group group)
        {
            var id = _groupRepository.Create(group);
            return StatusCode(StatusCodes.Status201Created, new { Id = id });
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateGroup([FromBody] Group group)
        {
            _groupRepository.Update(group);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteGroup(int id)
        {
            _groupRepository.Delete(id);
            return NoContent();
        }
    }
}
