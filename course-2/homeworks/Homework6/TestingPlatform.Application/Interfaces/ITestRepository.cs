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
        Task<IEnumerable<TestDto>> GetAllForStudentAsync(int studentId);
        Task<IEnumerable<TestDto>> GetTopRecentAsync(int count = 5);
        Task<IEnumerable<object>> GetTestCountByAsync();
        Task<IEnumerable<object>> GetCourseStatsAsync();
        Task<IEnumerable<object>> GetDirectionAveragesAsync();
        Task<IEnumerable<object>> GetTimelineByPublicAsync();
        Task<IEnumerable<object>> GetTopGroupsByTestCountAsync(int top = 10);
    }
}
