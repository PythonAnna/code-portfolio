using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Mappings
{
    internal class DirectionProfile: Profile
    {
        public DirectionProfile()
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}
