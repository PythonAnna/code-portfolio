using Microsoft.EntityFrameworkCore;
using TestingPlatform.Models;

namespace TestingPlatform.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Student> Students { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Login = "vanya",
                    Email="vanya@mail.ru",
                    FirstName="Иванов",
                    MiddleName="Иван",
                    LastName="Иванович",
                    Phone="+7(900)111-22-33",
                    VkProfileLink="",
                    CreatedAt = DateTime.Now
                },
                new Student
                {
                    Id = 2,
                    Login = "marina",
                    Email = "marina@mail.ru",
                    FirstName = "",
                    MiddleName = "",
                    LastName = "",
                    Phone = "",
                    VkProfileLink = "",
                    CreatedAt = DateTime.Now
                });
        }
    }
}
