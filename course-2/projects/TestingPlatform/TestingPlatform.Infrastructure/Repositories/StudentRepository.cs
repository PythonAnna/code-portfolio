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
using Microsoft.Extensions.Logging;


namespace TestingPlatform.Infrastructure.Repositories
{
    public class StudentRepository(AppDbContext _db, IMapper mapper, ILogger<StudentRepository> logger) : IStudentRepository
    {
        public async Task<int> CreateAsync(StudentDto studentDto)
        {
            logger.LogInformation("Создание нового студента");
            var student = mapper.Map<Student>(studentDto);

            var studentId = await _db.AddAsync(student);
            await _db.SaveChangesAsync();

            logger.LogInformation("Студент успешно создан с Id={StudentId}", studentId.Entity.Id);
            return studentId.Entity.Id;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            logger.LogInformation("Получен запрос на получение всех студентов");
            var students = await _db.Students
                .Include(s => s.User)
                .ToListAsync();

            logger.LogInformation($"Из БД получено {students.Count} студентов");
            return mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetStudentsById(int id)
        {
            logger.LogInformation($"Получен запрос на получение студента Id = {id}");
            var student = await _db.Students
                .Include(s => s.User)
                .Include(s => s.Tests)
                .AsNoTracking()
                .FirstOrDefaultAsync(student => student.Id == id);
            if(student == null)
            {
                throw new EntityNotFoundException("Студент не найден");
            }

            logger.LogInformation($"Студент Id = {id} успешно найден");
            return mapper.Map<StudentDto>(student);
        }

        public async Task UpdateAsync(StudentDto studentDto)
        {
            logger.LogInformation("Обновление студента Id={StudentId}", studentDto.Id);
            var student = await _db.Students.FirstOrDefaultAsync(student => student.Id == studentDto.Id);
            if(student == null)
            {
                throw new EntityNotFoundException("Студент не найден");
            }
            student.Phone = studentDto.Phone;
            student.VkProfileLink = studentDto.VkProfileLink;

            await _db.SaveChangesAsync();
            logger.LogInformation("Студент Id={StudentId} успешно обновлен", studentDto.Id);
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Удаление студента Id={StudentId}", id);
            var student = _db.Students
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == id);
            if(student == null)
            {
                throw new EntityNotFoundException("Студен не найден");
            }
            _db.Users.Remove(student.User);
            await _db.SaveChangesAsync();
            logger.LogInformation("Студент Id={StudentId} успешно удален", id);
        }
    }
}
