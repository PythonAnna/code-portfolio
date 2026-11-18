using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DirectionId { get; set; }
        public int ProjectId { get; set; }
        public int CourseId { get; set; }
    }
}
