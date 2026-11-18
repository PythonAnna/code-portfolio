using TestingPlatform.Enum;

namespace TestingPlatform.API.Responses
{
    public class TestResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
    }
}
