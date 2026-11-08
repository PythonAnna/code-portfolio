using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace TestingPlatform.API.Requests.Student
{
    public class UpdateStudentRequest
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string VkProfileLink { get; set; }
    }
}
