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
        public int Create(GroupDto groupDto)
        {
            var group = mapper.Map<Domain.Models.Group>(groupDto);

            var direction = _db.Directions.FirstOrDefault(d => d.Id == group.DirectionId);
            if (direction is null)
            {
                throw new Exception("Направление не найдено");
            }
            group.Direction = direction;

            var course = _db.Courses.FirstOrDefault(c => c.Id == group.CourseId);
            if(course is null)
            {
                throw new Exception("Курс не найден");
            }
            group.Course = course;

            var project = _db.Projects.FirstOrDefault(p => p.Id == group.ProjectId);
            if(project is null)
            {
                throw new Exception("Проект не найден");
            }
            group.Project = project;

            var groupId = _db.Add(group);
            _db.SaveChanges();

            return groupId.Entity.Id;
        }

        public List<GroupDto> GetAll()
        {
            var groups = _db.Groups
                .Include(g => g.Project)
                .Include(g => g.Direction)
                .Include(g => g.Course)
                .Include(g => g.Students)
                .AsNoTracking()
                .ToList();

            return mapper.Map<List<GroupDto>>(groups);
        }

        public GroupDto GetById(int id)
        {
            var group = _db.Groups
                .Include(g => g.Project)
                .Include(g => g.Course)
                .Include(g => g.Direction)
                .AsNoTracking()
                .FirstOrDefault(g => g.Id == id);

            if(group is null)
            {
                throw new Exception("Группа не найдена!");
            }
            return mapper.Map<GroupDto>(group);
        }

        public void Update(GroupDto group)
        {
            var exists = _db.Groups.FirstOrDefault(g => g.Id == group.Id);

            if(exists is null)
            {
                throw new Exception("Группа не найдена");
            }

            exists.Name = group.Name;
            exists.DirectionId = group.Direction.Id;
            exists.CourseId = group.Course.Id;
            exists.ProjectId = group.Project.Id;

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var group = _db.Groups.FirstOrDefault(g => g.Id == id);
            if(group is null)
            {
                throw new Exception("Группа не найдена");
            }

            _db.Groups.Remove(group);
            _db.SaveChanges();
        }
    }
}
