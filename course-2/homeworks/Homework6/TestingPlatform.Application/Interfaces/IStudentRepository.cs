using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentDto>> GetAllStudents();
        Task<StudentDto> GetStudentsById(int id);
        Task<int> CreateAsync(StudentDto studentDto);
        Task UpdateAsync(StudentDto studentDto);
        Task DeleteAsync(int id);
    }
}
