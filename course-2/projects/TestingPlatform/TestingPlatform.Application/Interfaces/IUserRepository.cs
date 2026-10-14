using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Models;

namespace TestingPlatform.Application.Interfaces
{
    public interface IUserRepository
    {
        int CreateAsync(User user);
        List<User> GetAllAsync();
        User GetByIdAsync(int id);
        void UpdateAsync(User user);
        void DeleteAsync(int id);
    }
}
