using AutoMapper;
using TestingPlatform.API.Requests.Question;
using TestingPlatform.API.Responses.Question;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.API.Mappings
{
    public class QuestionProfile: Profile
    {
        public QuestionProfile()
        {
            CreateMap<QuestionDto, QuestionResponse>();
            CreateMap<CreateQuestionRequest, QuestionDto>();
            CreateMap<UpdateQuestionRequest, QuestionDto>();
        }
    }
}
