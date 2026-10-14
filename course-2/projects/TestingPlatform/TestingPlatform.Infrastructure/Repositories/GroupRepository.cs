using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Models;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class GroupRepository(AppDbContext _db) : IGroupRepository
    {
        public int Create(Group group)
        {
            var direction = _db.Directions.Any(d => d.Id == group.DirectionId);
            var course = _db.Courses.Any(c => c.Id == group.CourseId);
            var project = _db.Projects.Any(p => p.Id == group.ProjectId);
            if(!direction || !course || !project)
            {
                throw new Exception("Убедитесь, что DirectionId, CourseId и ProjectId существуют");
            }

            if (group.Students.Any())
            {
                var ids = group.Students.Select(s => s.Id).ToList();
                var students = _db.Students.Where(s => ids.Contains(s.Id)).ToList();
                if (students.Count != ids.Count)
                {
                    throw new Exception("Некоторые студенты не найдены");
                }
                group.Students = students;
            }

            var groupId = _db.Add(group);
            _db.SaveChanges();

            return groupId.Entity.Id;
        }

        public List<Group> GetAll()
        {
            var groups = _db.Groups.ToList();
            return groups;
        }

        public Group GetById(int id)
        {
            var group = _db.Groups.FirstOrDefault(g => g.Id == id);
            if(group is null)
            {
                throw new Exception("Группа не найдена!");
            }
            return group;
        }

        public void Update(Group group)
        {
            var exists = _db.Groups.FirstOrDefault(g => g.Id == group.Id);

            if(exists is null)
            {
                throw new Exception("Группа не найдена");
            }

            var directionOk = _db.Directions.Any(d => d.Id == group.DirectionId);
            var courseOk = _db.Courses.Any(c => c.Id == group.CourseId);
            var projectOk = _db.Projects.Any(p => p.Id == group.ProjectId);

            if(!directionOk || !courseOk || !projectOk)
            {
                throw new Exception("Убедитесь, что DirectionId, CourseId и ProjectId существуют");
            }

            if (group.Students.Any())
            {
                var ids = group.Students.Select(s => s.Id).ToList();
                var students = _db.Students.Where(s => ids.Contains(s.Id)).ToList();

                if(students.Count != ids.Count)
                {
                    throw new Exception("Некоторые студенты не найдены!");
                }
                exists.Students = group.Students;
            }

            exists.Name = group.Name;
            exists.DirectionId = group.DirectionId;
            exists.CourseId = group.CourseId;
            exists.ProjectId = group.ProjectId;

            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            var group = _db.Groups.FirstOrDefault(g => g.Id == id);
            if(group is null)
            {
                throw new Exception("Группа не найдена");
            }

            _db.Remove(group);
            _db.SaveChanges();
        }
    }
}
