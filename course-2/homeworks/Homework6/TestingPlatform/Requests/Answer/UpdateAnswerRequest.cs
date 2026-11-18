namespace TestingPlatform.API.Requests.Answer
{
    public class UpdateAnswerRequest
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
    }
}
