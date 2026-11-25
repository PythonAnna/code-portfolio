using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    public class QuestionRepository(AppDbContext _db, IMapper mapper, ILogger<QuestionRepository> logger) : IQuestionRepository
    {
        public async Task<int> CreateAsync(QuestionDto questionDto)
        {
            logger.LogInformation("Создание нового вопроса для теста TestId={TestId}", questionDto.TestId);
            var question = mapper.Map<Question>(questionDto);

            var questionId = await _db.AddAsync(question);
            await _db.SaveChangesAsync();

            logger.LogInformation("Вопрос успешно создан с Id={QuestionId}", questionId.Entity.Id);
            return questionId.Entity.Id;
        }

        public async Task<IEnumerable<QuestionDto>> GetAllAsync()
        {
            logger.LogInformation("Получен запрос на получение всех вовпросов");
            var questions = await _db.Questions.AsNoTracking().ToListAsync();
            
            logger.LogInformation($"Из БД получено {questions.Count} вопросов");
            return mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<QuestionDto> GetByIdAsync(int id)
        {
            logger.LogInformation($"Получен запрос на получение вопроса Id = {id}");
            var question = await _db.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(question => question.Id == id);

            if(question == null)
            {
                throw new EntityNotFoundException("Вопрос не найден");
            }

            logger.LogInformation($"Вопрос Id = {id} успешно найден");
            return mapper.Map<QuestionDto>(question);
        }

        public async Task UpdateAsync(QuestionDto questionDto)
        {
            logger.LogInformation("Обновление вопроса Id={QuestionId}", questionDto.Id);
            var question = await _db.Questions.FirstOrDefaultAsync(question => question.Id == questionDto.Id);

            if(question == null)
            {
                throw new EntityNotFoundException("Вопрос не найден");
            }

            question.AnswerType = questionDto.AnswerType;
            question.Text = questionDto.Text;
            question.MaxScore = questionDto.MaxScore;
            question.Description = questionDto.Description;
            question.IsScoring = questionDto.IsScoring;
            question.TestId = questionDto.TestId;

            await _db.SaveChangesAsync();
            logger.LogInformation("Вопрос Id={QuestionId} успешно обновлен", questionDto.Id);
        }
        public async Task DeleteAsync(int id)
        {
            logger.LogInformation("Удаление вопроса Id={QuestionId}", id);
            var question = await _db.Questions.FirstOrDefaultAsync(question => question.Id == id);

            if(question == null)
            {
                throw new EntityNotFoundException("Вопрос не найден");
            }

            _db.Questions.Remove(question);
            await _db.SaveChangesAsync();

            logger.LogInformation("Вопрос Id={QuestionId} успешно удален", id);
        }

    }
}
