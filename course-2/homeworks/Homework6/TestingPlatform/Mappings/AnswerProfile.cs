using AutoMapper;
using TestingPlatform.API.Requests.Answer;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Infrastructure.Repositories;

namespace TestingPlatform.API.Mappings
{
    public class AnswerProfile: Profile
    {
        public AnswerProfile()
        {
            CreateMap<AnswerDto, AnswerRepository>();
            CreateMap<CreateAnswerRequest, AnswerDto>();
            CreateMap<UpdateAnswerRequest, AnswerDto>();
        }
    }
}
