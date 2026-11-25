using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Infrastructure.Exceptions;
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
            await UpdateMemberTest(test, testDto);
            await _db.SaveChangesAsync();
            return testId.Entity.Id;
        }

        public async Task<IEnumerable<TestDto>> GetAllTests(bool? IsPublic, List<int> groupIds, List<int> studentId)
        {
            var tests = _db.Tests
                .OrderByDescending(t => t.PublishedAt)
                .AsNoTracking()
                .AsQueryable();

            if (IsPublic != null)
            {
                tests = tests.Where(t => t.IsPublic);
            }

            if (groupIds.Any())
            {
                tests = tests.Where(t => t.Groups.Any(g => groupIds.Contains(g.Id)));
            }

            if (studentId.Any())
            {
                tests = tests.Where(t => t.Students.Any(s => studentId.Contains(s.Id)));
            }

            await RefreshPublicationStatuses();
            return mapper.Map<IEnumerable<TestDto>>(await tests.ToListAsync());
        }

        public async Task<TestDto> GetTestById(int id)
        {
            var test = await _db.Tests
                .Include(test => test.Directions)
                .Include(test => test.Courses)
                .Include(test => test.Groups)
                .Include(test => test.Projects)
                .Include(test => test.Students)
                    .ThenInclude(student => student.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(test => test.Id == id);

            if (test == null)
            {
                throw new EntityNotFoundException("Тест не найден");
            }

            await RefreshPublicationStatuses();
            return mapper.Map<TestDto>(test);
        }

        public async Task UpdateTest(TestDto testDto)
        {
            var test = await _db.Tests.FirstOrDefaultAsync(test => test.Id == testDto.Id);

            if (test == null)
            {
                throw new EntityNotFoundException("Тест не найден");
            }

            test.Title = testDto.Title;
            test.Description = testDto.Description;
            test.IsRepeatable = testDto.IsRepeatable;
            test.Type = testDto.Type;
            test.PublishedAt = testDto.PublishedAt;
            test.Deadline = testDto.Deadline;
            test.DurationMinutes = testDto.DurationMinutes;
            test.IsPublic = testDto.IsPublic;
            test.PassingScore = testDto.PassingScore;
            test.MaxAttempts = testDto.MaxAttempts;

            await UpdateMemberTest(test, testDto);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteTest(int id)
        {
            var test = await _db.Tests.FirstOrDefaultAsync(test => test.Id == id);

            if (test == null)
            {
                throw new EntityNotFoundException("Тест не найден");
            }

            _db.Tests.Remove(test);
            await _db.SaveChangesAsync();
        }

        //public async Task<List<TestDto>> GetAllForStudentAsync(int studentId)
        //{
        //    var tests = await _db.Tests
        //        .Where(t => t.IsPublic)
        //        .Where(
        //            t => t.Students.Any(s => s.Id == studentId)
        //        || t.Courses.Any(c => c.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
        //        || t.Projects.Any(p => p.Groups.Any(g => g.Students.Any(s => s.Id == studentId)))
        //        || t.Directions.Any(d => d.Groups.Any(g => g.Students.Any(s => s.Id == studentId))))
        //        .ToListAsync();

        //    await RefreshPublicationStatuses();
        //    return mapper.Map<List<TestDto>>(tests);
        //}

        //public async Task<IEnumerable<TestDto>> GetTopRecentAsync(int count = 5)
        //{
        //    var tests = await _db.Tests.AsNoTracking()
        //        .OrderByDescending(t => t.PublishedAt)
        //        .ThenByDescending(t => t.Id)
        //        .Take(count) 
        //        .ToListAsync();

        //    return mapper.Map<IEnumerable<TestDto>>(tests);
        //}

        //public async Task<IEnumerable<object>> GetTestCountByAsync()
        //{
        //    return await _db.Tests
        //        .AsNoTracking()
        //        .GroupBy(t => t.Type)
        //        .Select(g => new
        //        {
        //            Type = g.Key,
        //            Count = g.Count()
        //        })
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<object>> GetCourseStatsAsync()
        //{
        //    return await _db.Courses
        //        .AsNoTracking()
        //        .Select(c => new
        //        {
        //            Course = c.Name,
        //            AvgDuration = c.Tests.Average(t => (double?)t.DurationMinutes) ?? 0,
        //            AvgPassingScore = c.Tests.Average(t => (double?)t.PassingScore) ?? 0
        //        })
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<object>> GetDirectionAveragesAsync()
        //{
        //    return await _db.Directions
        //        .AsNoTracking()
        //        .Select(d => new
        //        {
        //            Direction = d.Name,
        //            AvgDuration = d.Tests.Average(t => (double?)t.DurationMinutes) ?? 0,
        //            AvgPassingScore = d.Tests.Average(t => (double?)t.PassingScore) ?? 0
        //        })
        //        .OrderByDescending(x => x.AvgPassingScore)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<object>> GetTimelineByPublicAsync()
        //{
        //    return await _db.Tests
        //        .AsNoTracking()
        //        .Where(t => t.PublishedAt != default)
        //        .GroupBy(t => new
        //        {
        //            t.IsPublic,
        //            Year = t.PublishedAt.Year,
        //            Month = t.PublishedAt.Month
        //        })
        //        .Select(g => new
        //        {
        //            g.Key.IsPublic,
        //            g.Key.Year,
        //            g.Key.Month,
        //            Count = g.Count()
        //        })
        //        .OrderBy(x => x.Month)
        //        .ThenByDescending(x => x.IsPublic)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<object>> GetTopGroupsByTestCountAsync(int top = 10)
        //{
        //    return await _db.Tests
        //        .AsNoTracking()
        //        .SelectMany(t => t.Groups.Select(g => new
        //        {
        //            Group = g.Name,
        //            TestId = t.Id
        //        }))
        //        .GroupBy(x => x.Group)
        //        .Select(g => new
        //        {
        //            Group = g.Key,
        //            TestCount = g.Select(x => x.TestId)
        //                .Distinct()
        //                .Count()
        //        })
        //        .OrderByDescending(x => x.TestCount)
        //        .Take(top)
        //        .ToListAsync();
        //}

        private async Task UpdateMemberTest(Test test, TestDto testDto)
        {
            var studentIds = testDto.Students?
                .Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();

            var groupIds = testDto.Groups?
                .Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();

            var courseIds = testDto.Courses?
                .Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();

            var directionIds = testDto.Directions?
                .Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();

            var projectIds = testDto.Projects?
                .Select(x => x.Id)
                .Where(id => id > 0)
                .Distinct()
                .ToArray() ?? Array.Empty<int>();

            if(_db.Entry(test).State == EntityState.Detached)
            {
                _db.Attach(test);
            }

            await _db.Entry(test).Collection(t => t.Students).LoadAsync();

            test.Students.Clear();

            if (studentIds.Length > 0)
            {
                var students = await _db.Students
                    .Where(s => studentIds.Contains(s.Id))
                    .ToListAsync();

                foreach (var student in students)
                {
                    test.Students.Add(student);
                }
            }

            await _db.Entry(test).Collection(t => t.Groups).LoadAsync();

            test.Groups.Clear();

            if(groupIds.Length > 0)
            {
                var groups = await _db.Groups
                    .Where(g => groupIds.Contains(g.Id))
                    .ToListAsync();

                foreach(var group in groups)
                {
                    test.Groups.Add(group);
                }
            }

            await _db.Entry(test).Collection(t => t.Courses).LoadAsync();

            test.Courses.Clear();

            if(courseIds.Length > 0)
            {
                var courses = await _db.Courses
                    .Where(c => courseIds.Contains(c.Id))
                    .ToListAsync();

                foreach(var course in courses)
                {
                    test.Courses.Add(course);
                }
            }

            await _db.Entry(test).Collection(t => t.Directions).LoadAsync();

            test.Directions.Clear();

            if (directionIds.Length > 0)
            {
                var directions = await _db.Directions
                    .Where(d => directionIds.Contains(d.Id))
                    .ToListAsync();

                foreach (var direction in directions)
                {
                    test.Directions.Add(direction);
                }
            }

            await _db.Entry(test).Collection(t => t.Projects).LoadAsync();

            test.Projects.Clear();

            if (projectIds.Length > 0)
            {
                var projects = await _db.Projects
                    .Where(p => projectIds.Contains(p.Id))
                    .ToListAsync();

                foreach (var project in projects)
                {
                    test.Projects.Add(project);
                }
            }
        }


        private async Task RefreshPublicationStatuses()
        {
            var now = DateTime.UtcNow;
            var publishCandidates = await _db.Tests
                .AsNoTracking()
                .Where(t => !t.IsPublic && (t.PublishedAt != null || t.Deadline != null))
                .Select(t => new { t.Id, t.PublishedAt, t.Deadline })
                .ToListAsync();

            var toPublishIds = publishCandidates
                .Where(x => x.PublishedAt != null
                            && x.PublishedAt <= now
                            && (x.Deadline == null || x.Deadline > now))
                .Select(x => x.Id)
                .ToList();

            if(toPublishIds.Count > 0)
            {
                await _db.Tests
                    .Where(t => toPublishIds.Contains(t.Id))
                    .ExecuteUpdateAsync(s => s.SetProperty(t => t.IsPublic, true));
            }

            var unpublishCandidates = await _db.Tests
                .AsNoTracking()
                .Where(t => t.IsPublic && (t.PublishedAt == null || t.Deadline != null))
                .Select(t => new { t.Id, t.PublishedAt, t.Deadline })
                .ToListAsync();

            var toUnpublishIds = unpublishCandidates
                .Where(x => x.PublishedAt == null || (x.Deadline != null && x.Deadline <= now))
                .Select(x => x.Id)
                .ToList();

            if(toUnpublishIds.Count > 0)
            {
                await _db.Tests
                    .Where(t => toUnpublishIds.Contains(t.Id))
                    .ExecuteUpdateAsync(s => s.SetProperty(t => t.IsPublic, false));
            }
        }
    }
}