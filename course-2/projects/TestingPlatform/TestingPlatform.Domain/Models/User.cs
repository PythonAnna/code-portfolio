using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;
using TestingPlatform.Enum;

namespace TestingPlatform.Models
{ 
    public class User
    {
        public int Id { get; set; }
        public string Login {  get; set; }
        public string PasswordHash { get; set; }
        public string Email {  get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public UserRole Role { get; set; }

        [JsonIgnore]
        public Student? Student { get; set; }
    }
}
