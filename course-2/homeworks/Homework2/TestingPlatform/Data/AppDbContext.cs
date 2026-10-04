using Microsoft.EntityFrameworkCore;
using TestingPlatform.Models;

namespace TestingPlatform.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Direction> Directions => Set<Direction>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Answer> Answers => Set<Answer>();
        public DbSet<Attempt> Attempts => Set<Attempt>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Test> Tests => Set<Test>();
        public DbSet<TestResult> TestResults => Set<TestResult>();
        public DbSet<UserAttemptAnswer> UserAttemptAnswers => Set<UserAttemptAnswer>();
        public DbSet<UserSelectedOption> UserSelectedOptions => Set<UserSelectedOption>();
        public DbSet<UserTextAnswer> UserTextAnswers => Set<UserTextAnswer>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasIndex(x => x.Login).IsUnique();
                e.HasIndex(x => x.Email).IsUnique();

                e.Property(x => x.PasswordHash).IsRequired();
                e.Property(x => x.Login).IsRequired();
                e.Property(x => x.Email).IsRequired();
                e.Property(x => x.FirstName).IsRequired();
                e.Property(x => x.LastName).IsRequired();

                e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

                e.HasOne(x => x.Student)
                    .WithOne(s => s.User)
                    .HasForeignKey<Student>(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.Property(x => x.Role).HasConversion<string>();
            });

            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Phone)
                    .IsRequired()
                    .HasMaxLength(30);

                e.Property(x => x.VkProfileLink).IsRequired();
                e.Property(x => x.UserId).IsRequired();

                e.HasIndex(x => x.UserId).IsUnique();
            });

            modelBuilder.Entity<Direction>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<Project>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();
            });

            modelBuilder.Entity<Group>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired();
                e.HasIndex(x => x.Name).IsUnique();

                e.Property(x => x.DirectionId).IsRequired();
                e.Property(x => x.CourseId).IsRequired();
                e.Property(x => x.ProjectId).IsRequired();

                e.HasOne(x => x.Direction)
                    .WithMany(d => d.Groups)
                    .HasForeignKey(x => x.DirectionId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Course)
                    .WithMany(c => c.Groups)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Project)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(x => x.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasMany(x => x.Students)
                    .WithMany(s => s.Groups)
                    .UsingEntity(j => j.ToTable("group_students"));
            });

            modelBuilder.Entity<Test>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Title).IsRequired();
                e.Property(x => x.Description).IsRequired();
                e.Property(x => x.PublishedAt).IsRequired();
                e.Property(x => x.Deadline).IsRequired();

                e.Property(x => x.Type).HasConversion<string>();

                e.Property(x => x.IsRepeatable).HasDefaultValue(false);
                e.Property(x => x.IsPublic).HasDefaultValue(false);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");


                e.HasMany(x => x.Students)
                    .WithMany(s => s.Tests)
                    .UsingEntity(j => j.ToTable("test_students"));

                e.HasMany(x => x.Projects)
                    .WithMany(p => p.Tests)
                    .UsingEntity(j => j.ToTable("test_projects"));

                e.HasMany(x => x.Courses)
                    .WithMany(c => c.Tests)
                    .UsingEntity(j => j.ToTable("test_courses"));

                e.HasMany(x => x.Groups)
                .WithMany(g => g.Tests)
                .UsingEntity(j => j.ToTable("test_groups"));

                e.HasMany(x => x.Directions)
                    .WithMany(d => d.Tests)
                    .UsingEntity(j => j.ToTable("test_directions"));

                e.HasMany(x => x.Questions)
                    .WithOne(q => q.Test)
                    .HasForeignKey(q => q.TestId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Question>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Text).IsRequired();
                e.Property(x => x.Number).IsRequired();
                e.Property(x => x.TestId).IsRequired();

                e.Property(x => x.Description).HasMaxLength(2000);
                e.Property(x => x.AnswerType).HasConversion<string>();
                e.Property(x => x.IsScoring).HasDefaultValue(true);

                e.HasIndex(x => new { x.TestId, x.Number }).IsUnique();
            });

            modelBuilder.Entity<Answer>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Text).IsRequired();
                e.Property(x => x.IsCorrect).IsRequired();
                e.Property(x => x.QuestionId).IsRequired();

                e.HasOne(x => x.Question)
                    .WithMany(q => q.Answers)
                    .HasForeignKey(x => x.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Attempt>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.StartedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                e.Property(x => x.TestId).IsRequired();
                e.Property(x => x.StudentId).IsRequired();

                e.HasOne(x => x.Test)
                    .WithMany(t => t.Attempts)
                    .HasForeignKey(x => x.TestId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Student)
                    .WithMany(s => s.Attempts)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<UserAttemptAnswer>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasIndex(x => new { x.AttemptId, x.QuestionId }).IsUnique();

                e.Property(x => x.AttemptId).IsRequired();
                e.Property(x => x.QuestionId).IsRequired();

                e.HasOne(x => x.Attempt)
                    .WithMany(a => a.UserAttemptAnswers)
                    .HasForeignKey(x => x.AttemptId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Question)
                    .WithMany(q => q.UserAttemptAnswers)
                    .HasForeignKey(x => x.QuestionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserSelectedOption>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.UserAttemptAnswerId).IsRequired();
                e.Property(x => x.AnswerId).IsRequired();

                e.HasOne(x => x.UserAttemptAnswer)
                    .WithMany(u => u.UserSelectedOptions)
                    .HasForeignKey(x => x.UserAttemptAnswerId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Answer)
                    .WithMany(a => a.UserSelectedOptions)
                    .HasForeignKey(x => x.AnswerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<UserTextAnswer>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.TextAnswer).IsRequired();
                e.Property(x => x.UserAttemptAnswerId).IsRequired();

                e.HasOne(x => x.UserAttemptAnswer)
                    .WithOne(u => u.UserTextAnswer)
                    .HasForeignKey<UserTextAnswer>(x => x.UserAttemptAnswerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TestResult>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasIndex(x => new { x.TestId, x.StudentId, x.AttemptId }).IsUnique();

                e.Property(x => x.Passed).IsRequired();
                e.Property(x => x.TestId).IsRequired();
                e.Property(x => x.AttemptId).IsRequired();
                e.Property(x => x.StudentId).IsRequired();

                e.HasOne(x => x.Test)
                    .WithMany(t => t.TestResults)
                    .HasForeignKey(x => x.TestId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Attempt)
                    .WithMany(a => a.TestResults)
                    .HasForeignKey(x => x.AttemptId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(x => x.Student)
                    .WithMany(s => s.TestResults)
                    .HasForeignKey(x => x.StudentId)
                    .OnDelete(DeleteBehavior.Cascade);

            });
        }
    }
}
