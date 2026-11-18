using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<AnswerDto>> GetAllAnswers();
        Task<AnswerDto> GetAnswersById(int id);
        Task<int> CreateAnswer(AnswerDto answerDto);
        Task UpdateAnswer(AnswerDto answerDto);
        Task DaleteAnswer(int id);
    }
}
