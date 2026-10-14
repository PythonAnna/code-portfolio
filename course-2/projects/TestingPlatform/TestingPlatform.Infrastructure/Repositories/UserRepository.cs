using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Models;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext _db): IUserRepository
    {
        public List<User> GetAllAsync()
        {
            var users = _db.Users.AsNoTracking().ToList();
            return users;
        }

        public User GetByIdAsync(int id)
        {
            var user = _db.Users.AsNoTracking().FirstOrDefault(u => u.Id == id);
            if (user is null)
                throw new Exception("Пользователь не найден");

            return user;
        }

        public int CreateAsync(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();

            return user.Id;
        }

        public void UpdateAsync(User user)
        {
            _db.SaveChanges();
        }

        public void DeleteAsync(int id)
        {
            var user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user is null)
                throw new Exception("Пользователь не найден");

            _db.Users.Remove(user);
            _db.SaveChanges();
        }
    }
}
