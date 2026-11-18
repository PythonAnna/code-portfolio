using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingPlatform.Application.Dtos;
using TestingPlatform.Domain.Models;

namespace TestingPlatform.Infrastructure.Mappings
{
    public class AnswerProfile: Profile
    {
        public AnswerProfile()
        {
            CreateMap<Answer, AnswerDto>().ReverseMap();
        }
    }
}
