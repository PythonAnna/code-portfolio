using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<int> CreateAsync(UserDto userDto);
        Task <IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task UpdateAsync(UserDto userDto);
        Task DeleteAsync(int id);
    }
}
