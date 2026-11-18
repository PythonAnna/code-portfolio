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

namespace TestingPlatform.Infrastructure.Repositories
{
    public class AnswerRepository(AppDbContext _db, IMapper mapper) : IAnswerRepository
    {
        public async Task<int> CreateAnswer(AnswerDto answerDto)
        {
            var answer = mapper.Map<Answer>(answerDto);
            var answerId = await _db.Answers.AddAsync(answer);
            await _db.SaveChangesAsync();
            return answerId.Entity.Id;
        }

        public async Task<IEnumerable<AnswerDto>> GetAllAnswers()
        {
            var answers = await _db.Answers.ToListAsync();

            return mapper.Map<IEnumerable<AnswerDto>>(answers);
        }

        public async Task<AnswerDto> GetAnswersById(int id)
        {
            var answer = await _db.Answers
                .AsNoTracking()
                .FirstOrDefaultAsync(answer => answer.Id == id);

            if(answer == null)
            {
                throw new Exception("Ответ не найден");
            }

            return mapper.Map<AnswerDto>(answer);
        }

        public async Task UpdateAnswer(AnswerDto answerDto)
        {
            var answer = await _db.Answers.FirstOrDefaultAsync(answer => answer.Id == answerDto.Id);
            if(answer == null)
            {
                throw new Exception("Ответ не найден");
            }
            answer.Text = answerDto.Text;
            answer.IsCorrect = answerDto.IsCorrect;
            await _db.SaveChangesAsync();
        }

        public async Task DaleteAnswer(int id)
        {
            var answer = await _db.Answers.FirstOrDefaultAsync(answer => answer.Id == id);
            if(answer == null)
            {
                throw new Exception("Ответ не найтед");
            }
            _db.Answers.Remove(answer);
            await _db.SaveChangesAsync();
        }
    }
}
