using Microsoft.EntityFrameworkCore;
using TestingPlatform.Models;

namespace TestingPlatform.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<User> User => Set<User>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Project> Projects => Set<Project>();
        public DbSet<Group> Groups => Set<Group>();
        public DbSet<Direction> Directions => Set<Direction>();
        public DbSet<Course> Courses => Set<Course>();

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

                e.Property(x => x.Login).IsRequired();
                e.Property(x => x.Email).IsRequired();

                e.Property(x => x.CreatedAt).HasDefaultValue("CURRENT_TIMESTAMP");

                e.HasOne(x => x.Student)
                    .WithOne(s => s.User)
                    .HasForeignKey<Student>(s => s.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Student>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.Phone)
                    .IsRequired()
                    .HasMaxLength(30);

                e.Property(x => x.VkProfileLink).IsRequired();
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

                e.HasOne(x => x.Direction)
                    .WithMany(d => d.Groups)
                    .HasForeignKey(x => x.DirectionId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Course)
                    .WithMany(d => d.Groups)
                    .HasForeignKey(x => x.CourseId)
                    .OnDelete(DeleteBehavior.Restrict);

                e.HasOne(x => x.Project)
                    .WithMany(d => d.Groups)
                    .HasForeignKey(x => x.ProjectId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
