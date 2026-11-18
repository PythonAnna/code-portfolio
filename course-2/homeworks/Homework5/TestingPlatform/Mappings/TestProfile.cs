using AutoMapper;
using TestingPlatform.API.Requests.Test;
using TestingPlatform.API.Responses;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Infrastructure.Repositories;

namespace TestingPlatform.API.Mappings
{
    public class TestProfile: Profile
    {
        public TestProfile()
        {
            CreateMap<TestDto, TestResponse>();
            CreateMap<CreateTestRequest, TestDto>();
            CreateMap<UpdateTestRequest, TestDto>();
        }
    }
}
