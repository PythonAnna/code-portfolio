using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public class QuestionRepository(AppDbContext _db, IMapper mapper) : IQuestionRepository
    {
        public async Task<int> CreateAsync(QuestionDto questionDto)
        {
            var question = mapper.Map<Question>(questionDto);

            var questionId = await _db.AddAsync(question);
            await _db.SaveChangesAsync();

            return questionId.Entity.Id;
        }

        public async Task<IEnumerable<QuestionDto>> GetAllAsync()
        {
            var questions = await _db.Questions.AsNoTracking().ToListAsync();
            return mapper.Map<IEnumerable<QuestionDto>>(questions);
        }

        public async Task<QuestionDto> GetByIdAsync(int id)
        {
            var question = await _db.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(question => question.Id == id);

            if(question == null)
            {
                throw new EntityNotFoundException("Вопрос не найден");
            }

            return mapper.Map<QuestionDto>(question);
        }

        public async Task UpdateAsync(QuestionDto questionDto)
        {
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
        }
        public async Task DeleteAsync(int id)
        {
            var question = await _db.Questions.FirstOrDefaultAsync(question => question.Id == id);

            if(question == null)
            {
                throw new EntityNotFoundException("Вопрос не найден");
            }

            _db.Questions.Remove(question);
            await _db.SaveChangesAsync();
        }

    }
}
