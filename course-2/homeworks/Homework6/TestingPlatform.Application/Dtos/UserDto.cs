using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TestingPlatform.Domain.Models;
using TestingPlatform.Enum;

namespace TestingPlatform.Application.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public UserRole Role { get; set; }
        public string Phone { get; set; }
        public string VkProfileLink { get; set; }
        public string Password { get; set; }
    }
}
