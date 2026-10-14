using System.ComponentModel.DataAnnotations;

namespace TestingPlatform.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Phone]
        public string Phone { get; set; }
        public string VkProfileLink { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Group> Groups { get; set; }
        public List<Test> Tests { get; set; }
        public List<Attempt> Attempts { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
