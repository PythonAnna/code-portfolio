using TestingPlatform.Enum;

namespace TestingPlatform.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text {  get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public bool IsScoring {  get; set; }
        public int? MaxScore { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public List<UserAttemptAnswer> UserAttemptAnswers { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
