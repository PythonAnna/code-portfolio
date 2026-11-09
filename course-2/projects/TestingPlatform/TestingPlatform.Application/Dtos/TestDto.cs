using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Domain.Models;
using TestingPlatform.Enum;

namespace TestingPlatform.Application.Dtos
{
    public class TestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRepeatable { get; set; } //Можно ли пройти повторно
        public TestType Type { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset PublishedAt { get; set; }
        public DateTimeOffset Deadline { get; set; }
        public int? DurationMinutes { get; set; } //время прохождения
        public bool IsPublic { get; set; }
        public int? PassingScore { get; set; } //проходной балл
        public int? MaxAttempts { get; set; }
        //public List<StudentDto> Students { get; set; }
        //public List<ProjectDto> Projects { get; set; }
        //public List<CourseDto> Courses { get; set; }
        //public List<GroupDto> Groups { get; set; }
        //public List<DirectionDto> Directions { get; set; }
    }
}
