using TestingPlatform.Domain.Enum;

namespace TestingPlatform.API.Requests.Test
{
    public class CreateTestRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRepeatable { get; set; }
        public TestType Type { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime Deadline { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsPublic { get; set; }
        public int? PassingScore { get; set; }
        public int? MaxAttempts { get; set; }
    }
}
