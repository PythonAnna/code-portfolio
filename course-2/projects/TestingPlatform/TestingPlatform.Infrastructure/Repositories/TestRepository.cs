using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class TestRepository(AppDbContext _db, IMapper mapper) : ITestRepository
    {
        public async Task<int> CreateTest(TestDto testDto)
        {
            var test = mapper.Map<Test>(testDto);
            var testId = await _db.AddAsync(test);
            await _db.SaveChangesAsync();
            return testId.Entity.Id;
        }

        public async Task<IEnumerable<TestDto>> GetAllTests(bool? IsPublic, List<int> groupIds, List<int> studentId)
        {
            var tests = _db.Tests
                .OrderByDescending(t => t.PublishedAt)
                .AsNoTracking()
                .AsQueryable();

            if(IsPublic != null)
            {
                tests = tests.Where(t => t.IsPublic);
            }

            if (groupIds.Any())
            {
                tests = tests.Where(t => t.Groups.Any(g => groupIds.Contains(g.Id)));
            }

            return mapper.Map<IEnumerable<TestDto>>(tests);
        }

        public async Task<TestDto> GetTestById(int id)
        {
            var test = await _db.Tests
                .AsNoTracking()
                .FirstOrDefaultAsync(test => test.Id == id);

            if(test == null)
            {
                throw new Exception("Тест не найден");
            }

            return mapper.Map<TestDto>(test);
        }

        public async Task UpdateTest(TestDto testDto)
        {
            var test = await _db.Tests.FirstOrDefaultAsync(test => test.Id == testDto.Id);

        }

        public async Task DeleteTest(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TestDto>> GetAllForStudentAsync(int studentId)
        {
            var tests = await _db.Tests
                .Where(t => t.IsPublic)
                .Where(
                    t => t.Students.Any(s => s.Id == studentId)
                ||  t.Courses.Any(c => c.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
                ||  t.Projects.Any(p => p.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
                ||  t.Directions.Any(d => d.Groups.Any(g => g.Students.Any(s => s.Id == studentId))))
                .ToListAsync();
            return mapper.Map<List<TestDto>>(tests);
        }
    }
}
