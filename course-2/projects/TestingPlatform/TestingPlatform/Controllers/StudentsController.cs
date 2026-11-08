using Microsoft.AspNetCore.Mvc;
using TestingPlatform.Application.Interfaces;
using AutoMapper;
using TestingPlatform.API.Responses;
using TestingPlatform.API.Requests.Student;
using TestingPlatform.Application.Dtos;


namespace TestingPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController(IStudentRepository studentRepository, IUserRepository userRepository, IMapper mapper) : ControllerBase
    {
        //private readonly AppDbContext _db;
        //public StudentsController(AppDbContext db)
        //{
        //    _db = db;
        //}
        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentRepository.GetAllStudents();
            return Ok(mapper.Map<IEnumerable<StudentResponse>>(students));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetStudentsById(int id)
        {
            var student = await studentRepository.GetStudentsById(id);

            return Ok(mapper.Map<StudentResponse>(student));
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] CreateStudentRequest student)
        {
            var userDto = new UserDto()
            {
                Login = student.Login,
                Password = student.Password,
                Email = student.Email,
                FirstName = student.FirstName,
                LastName = student.LastName,
                MiddleName = student.MiddleName,
                Role = Enum.UserRole.Student
            };

            var userId = await userRepository.CreateAsync(userDto);

            var studentDto = new StudentDto()
            {
                UserId = userId,
                Phone = student.Phone,
                VkProfileLink = student.VkProfileLink
            };

            var studentId = await studentRepository.CreateAsync(studentDto);

            return StatusCode(StatusCodes.Status201Created, new {Id = studentId});
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStudent([FromBody] UpdateStudentRequest student)
        {
            await studentRepository.UpdateAsync(mapper.Map<StudentDto>(student));
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await studentRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
