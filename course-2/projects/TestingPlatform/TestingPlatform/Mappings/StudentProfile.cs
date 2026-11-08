using AutoMapper;
using TestingPlatform.API.Requests.Student;
using TestingPlatform.API.Responses;
using TestingPlatform.Application.Dtos;

namespace TestingPlatform.API.Mappings
{
    public class StudentProfile: Profile
    {
        public StudentProfile()
        {
            CreateMap<StudentDto, StudentResponse>()
                .ForMember(d => d.Login, m => m.MapFrom(s => s.User.Login))
                .ForMember(d => d.Email, m => m.MapFrom(s => s.User.Email))
                .ForMember(d => d.FirstName, m => m.MapFrom(s => s.User.FirstName))
                .ForMember(d => d.MiddleName, m => m.MapFrom(s => s.User.MiddleName))
                .ForMember(d => d.LastName, m => m.MapFrom(s => s.User.LastName))
                .ForMember(d => d.Phone, m => m.MapFrom(s => s.User.Phone))
                .ForMember(d => d.VkProfileLink, m => m.MapFrom(s => s.User.VkProfileLink));
            CreateMap<UpdateStudentRequest, StudentDto>();
        }
    }
}
