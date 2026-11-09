using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface ITestRepository
    {
        Task<IEnumerable<TestDto>> GetAllTests(bool? IsPublic, List<int> groupIds, List<int> studentId);
        Task<TestDto> GetTestById(int id);
        Task<int> CreateTest(TestDto testDto);
        Task UpdateTest(TestDto testDto);
        Task DeleteTest(int id);
    }
}
