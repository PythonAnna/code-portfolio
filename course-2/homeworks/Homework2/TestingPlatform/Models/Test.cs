using TestingPlatform.Enum;

namespace TestingPlatform.Models
{
    public class Test
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
        public List<Question> Questions { get; set; }
        public List<Student> Students { get; set; }
        public List<Project> Projects { get; set; }
        public List<Course> Courses { get; set; }
        public List<Group> Groups { get; set; }
        public List<Direction> Directions { get; set; }
        public List<Attempt> Attempts { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
