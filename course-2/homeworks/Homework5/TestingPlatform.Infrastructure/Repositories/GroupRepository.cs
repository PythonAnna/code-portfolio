using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class GroupRepository(AppDbContext _db, IMapper mapper) : IGroupRepository
    {
        public async Task<int> CreateAsync(GroupDto groupDto)
        {
            var group = mapper.Map<Domain.Models.Group>(groupDto);

            var direction = await _db.Directions.FirstOrDefaultAsync(d => d.Id == group.DirectionId);
            if (direction is null)
            {
                throw new Exception("Направление не найдено");
            }
            group.Direction = direction;

            var course = await _db.Courses.FirstOrDefaultAsync(c => c.Id == group.CourseId);
            if (course is null)
            {
                throw new Exception("Курс не найден");
            }
            group.Course = course;

            var project = await _db.Projects.FirstOrDefaultAsync(p => p.Id == group.ProjectId);
            if (project is null)
            {
                throw new Exception("Проект не найден");
            }
            group.Project = project;

            //if (group.Students.Any())
            //{
            //    var ids = group.Students.Select(s => s.Id).ToList();
            //    var students = _db.Students.Where(s => ids.Contains(s.Id)).ToList();
            //    if (students.Count != ids.Count)
            //    {
            //        throw new Exception("Некоторые студенты не найдены");
            //    }
            //    group.Students = students;
            //}

            var groupId = _db.Add(group);
            _db.SaveChanges();

            return groupId.Entity.Id;
        }

        public async Task<List<GroupDto>> GetAllAsync()
        {
            var groups = await _db.Groups
                .Include(g => g.Project)
                .Include(g => g.Direction)
                .Include(g => g.Course)
                .Include(g => g.Students)
                .AsNoTracking()
                .ToListAsync();

            return mapper.Map<List<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            var group = await _db.Groups
                .Include(g => g.Project)
                .Include(g => g.Course)
                .Include(g => g.Direction)
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == id);

            if (group is null)
            {
                throw new Exception("Группа не найдена!");
            }
            return mapper.Map<GroupDto>(group);
        }

        public async Task UpdateAsync(GroupDto group)
        {
            var exists = await _db.Groups.FirstOrDefaultAsync(g => g.Id == group.Id);

            if (exists is null)
            {
                throw new Exception("Группа не найдена");
            }

            //var directionOk = _db.Directions.Any(d => d.Id == group.DirectionId);
            //var courseOk = _db.Courses.Any(c => c.Id == group.CourseId);
            //var projectOk = _db.Projects.Any(p => p.Id == group.ProjectId);

            //if(!directionOk || !courseOk || !projectOk)
            //{
            //    throw new Exception("Убедитесь, что DirectionId, CourseId и ProjectId существуют");
            //}

            //if (group.Students.Any())
            //{
            //    var ids = group.Students.Select(s => s.Id).ToList();
            //    var students = _db.Students.Where(s => ids.Contains(s.Id)).ToList();

            //    if(students.Count != ids.Count)
            //    {
            //        throw new Exception("Некоторые студенты не найдены!");
            //    }
            //    exists.Students = group.Students;
            //}

            exists.Name = group.Name;
            exists.DirectionId = group.Direction.Id;
            exists.CourseId = group.Course.Id;
            exists.ProjectId = group.Project.Id;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var group = await _db.Groups.FirstOrDefaultAsync(g => g.Id == id);
            if (group is null)
            {
                throw new Exception("Группа не найдена");
            }

            _db.Groups.Remove(group);
            await _db.SaveChangesAsync();
        }
    }
}
