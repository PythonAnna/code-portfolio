using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Application.Dtos;
using Microsoft.EntityFrameworkCore;


namespace TestingPlatform.Infrastructure.Repositories
{
    public class StudentRepository(AppDbContext _db, IMapper mapper) : IStudentRepository
    {
        public async Task<int> CreateAsync(StudentDto studentDto)
        {
            var student = mapper.Map<Student>(studentDto);

            var studentId = await _db.AddAsync(student);
            await _db.SaveChangesAsync();

            return studentId.Entity.Id;
        }

        public async Task<IEnumerable<StudentDto>> GetAllStudents()
        {
            var students = await _db.Students
                .Include(s => s.User)
                .ToListAsync();
            return mapper.Map<IEnumerable<StudentDto>>(students);
        }

        public async Task<StudentDto> GetStudentsById(int id)
        {
            var student = await _db.Students
                .Include(s => s.User)
                .Include(s => s.Tests)
                .AsNoTracking()
                .FirstOrDefaultAsync(student => student.Id == id);
            if(student == null)
            {
                throw new Exception("Студен не найден");
            }
            return mapper.Map<StudentDto>(student);
        }

        public async Task UpdateAsync(StudentDto studentDto)
        {
            var student = await _db.Students.FirstOrDefaultAsync(student => student.Id == studentDto.Id);
            if(student == null)
            {
                throw new Exception("Студент не найден");
            }
            student.Phone = studentDto.Phone;
            student.VkProfileLink = studentDto.VkProfileLink;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = _db.Students
                .Include(s => s.User)
                .FirstOrDefault(s => s.Id == id);
            if(student == null)
            {
                throw new Exception("Студен не найден");
            }
            _db.Users.Remove(student.User);
            await _db.SaveChangesAsync();
        }
    }
}
