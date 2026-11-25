using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TestingPlatform.Infrastructure.Exceptions;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext _db, IMapper mapper, ILogger<UserRepository> logger) : IUserRepository
    {
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            logger.LogInformation("Получен запрос на получение всех пользователей");
            var users = await _db.Users.AsNoTracking().ToListAsync();

            logger.LogInformation($"Из БД получено {users.Count} пользователей");
            return mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            logger.LogInformation($"Получен запрос на получение пользователя Id = {id}");
            var user = await _db.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new EntityNotFoundException("Пользователь не найден");

            logger.LogInformation($"Пользователь Id = {id} успешно найден");
            return mapper.Map<UserDto>(user);
        }

        public async Task<int> CreateAsync(UserDto userDto)
        {
            logger.LogInformation("Создание нового пользователя");
            var user = mapper.Map<User>(userDto);

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.MiddleName = userDto.MiddleName;
            user.PasswordHash = userDto.Password;


            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            logger.LogInformation("Пользователь успешно создан с Id={UserId}", userDto.Id);
            return user.Id;
        }

        public async Task UpdateAsync(UserDto userDto)
        {
            logger.LogInformation("Обновление пользователя Id={UserId}", userDto.Id);
            var user = await _db.Users.FirstOrDefaultAsync(user => user.Id == userDto.Id);

            if(user == null)
            {
                throw new EntityNotFoundException("Пользователь не найден");
            }

            if (_db.Users.Any(u => u.Login == userDto.Login))
                throw new ArgumentException($"Пользователь с логином {userDto.Login} уже существует");


            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.MiddleName = userDto.MiddleName;
            user.Role = userDto.Role;

            await _db.SaveChangesAsync();
            logger.LogInformation("Пользователь Id={UserId} успешно обновлен", userDto.Id);
        }

        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Удаление пользователя Id={UserId}", id);
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
                throw new EntityNotFoundException("Пользователь не найден");

            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            logger.LogInformation("Пользователь Id={UserId} успешно удален", id);
        }
    }
}
