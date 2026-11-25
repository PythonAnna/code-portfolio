using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionDto>> GetAllAsync();
        Task<QuestionDto> GetByIdAsync(int id);
        Task<int> CreateAsync(QuestionDto questionDto);
        Task UpdateAsync(QuestionDto questionDto);
        Task DeleteAsync(int id);
    }
}
