using Microsoft.EntityFrameworkCore;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Domain.Models;
using TestingPlatform.Application.Dtos;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext _db, IMapper mapper) : IUserRepository
    {
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _db.Users.AsNoTracking().ToListAsync();
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new Exception("Пользователь не найден");

            return mapper.Map<UserDto>(user);
        }

        public async Task<int> CreateAsync(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.MiddleName = userDto.MiddleName;
            user.PasswordHash = userDto.Password;


            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user.Id;
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Id == userDto.Id);

            if(user == null)
            {
                throw new Exception("Пользователь не найден");
            }

            if (_db.Users.Any(u => u.Login == userDto.Login))
                throw new Exception($"Пользователь с логином {userDto.Login} уже существует");


            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.MiddleName = userDto.MiddleName;
            user.Role = userDto.Role;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new Exception("Пользователь не найден");

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
        }
    }
}
