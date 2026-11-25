using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.Application.Interfaces
{
    public interface IGroupRepository
    {
        Task<int> CreateAsync(GroupDto group);
        Task<List<GroupDto>> GetAllAsync();
        Task<GroupDto> GetByIdAsync(int id);
        Task UpdateAsync(GroupDto group);
        Task DeleteAsync(int id);
    }
}
