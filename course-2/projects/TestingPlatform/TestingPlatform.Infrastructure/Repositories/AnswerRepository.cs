using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Application.Interfaces;
using TestingPlatform.Domain.Models;
using TestingPlatform.Infrastructure.Data;
using TestingPlatform.Infrastructure.Exceptions;

namespace TestingPlatform.Infrastructure.Repositories
{
    public class AnswerRepository(AppDbContext _db, IMapper mapper, ILogger<AnswerRepository> logger) : IAnswerRepository
    {
        public async Task<int> CreateAnswer(AnswerDto answerDto)
        {
            logger.LogInformation("Создание нового ответа");
            var answer = mapper.Map<Answer>(answerDto);

            var answerId = await _db.Answers.AddAsync(answer);
            await _db.SaveChangesAsync();

            logger.LogInformation("Ответ успешно создан с Id={AnswerId}", answerId.Entity.Id);
            return answerId.Entity.Id;
        }

        public async Task<IEnumerable<AnswerDto>> GetAllAnswers()
        {
            logger.LogInformation("Получен запрос на получение всех ответов");
            var answers = await _db.Answers.ToListAsync();

            logger.LogInformation($"Из БД получено {answers.Count} ответов");
            return mapper.Map<IEnumerable<AnswerDto>>(answers);
        }

        public async Task<AnswerDto> GetAnswersById(int id)
        {
            logger.LogInformation($"Получен запрос на получение ответа Id = {id}");
            var answer = await _db.Answers
                .AsNoTracking()
                .FirstOrDefaultAsync(answer => answer.Id == id);

            if(answer == null)
            {
                throw new EntityNotFoundException("Ответ не найден");
            }

            logger.LogInformation($"Ответ Id = {id} успешно найден");
            return mapper.Map<AnswerDto>(answer);
        }

        public async Task UpdateAnswer(AnswerDto answerDto)
        {
            logger.LogInformation("Обновление ответа Id={AnswerId}", answerDto.Id);
            var answer = await _db.Answers.FirstOrDefaultAsync(answer => answer.Id == answerDto.Id);
            if(answer == null)
            {
                throw new EntityNotFoundException("Ответ не найден");
            }
            answer.Text = answerDto.Text;
            answer.IsCorrect = answerDto.IsCorrect;
            await _db.SaveChangesAsync();
            logger.LogInformation("Ответ Id={AnswerId} успешно обновлен", answerDto.Id);
        }

        public async Task DeleteAnswer(int id)
        {
            logger.LogInformation("Удаление ответа Id={AnswerId}", id);
            var answer = await _db.Answers.FirstOrDefaultAsync(answer => answer.Id == id);
            if(answer == null)
            {
                throw new EntityNotFoundException("Ответ не найтед");
            }
            _db.Answers.Remove(answer);
            await _db.SaveChangesAsync();
            logger.LogInformation("Ответ Id={AnswerId} успешно удален", id);
        }
    }
}
