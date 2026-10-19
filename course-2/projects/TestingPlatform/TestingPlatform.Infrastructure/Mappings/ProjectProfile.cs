using AutoMapper;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Mappings
{
    internal class ProjectProfile: Profile
    {
        public ProjectProfile() 
        {
            CreateMap<Project, ProjectDto>().ReverseMap();
        }
    }
}
