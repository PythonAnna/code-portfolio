namespace TestingPlatform.API.Responses.Group
{
    public class GroupResponse: BaseResponse
    {
        public DirectionResponse Direction { get; set; }
        public CourseResponse Course { get; set; }
        public ProjectResponse Project { get; set; }
    }
}
