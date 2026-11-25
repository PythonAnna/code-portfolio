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
    internal class QuestionProfile: Profile
    {
        public QuestionProfile()
        {
            CreateMap<Question, QuestionDto>().ReverseMap();
        }
    }
}
