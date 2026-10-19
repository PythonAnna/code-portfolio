using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DirectionDto Direction { get; set; }
        public CourseDto Course { get; set; }
        public ProjectDto Project { get; set; }
        public List<Student> Students { get; set; }
        public List<Test> Tests { get; set; }
    }
}
