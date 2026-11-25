using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Domain.Enum;

namespace TestingPlatform.Application.Dtos
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public AnswerType AnswerType { get; set; }
        public bool IsScoring { get; set; }
        public int? MaxScore { get; set; }
        public int TestId { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
