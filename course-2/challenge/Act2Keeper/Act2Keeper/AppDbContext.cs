using Microsoft.EntityFrameworkCore;
using Act2Keeper.Models;

namespace Act2Keeper
{
    public class AppDbContext: DbContext
    {
        public DbSet<Visitor> Visitors => Set<Visitor>();
        public DbSet<VisitorAtionsHistory> Histories => Set<VisitorAtionsHistory>();
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitor>(e =>
            {
                e.HasKey(x => x.Id);

                e.Property(x => x.LevelOfTrust)
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<VisitorAtionsHistory>(e =>
            {
                e.HasKey(x => x.Id);

                e.HasOne(x => x.Visitor)
                    .WithMany(v => v.History)
                    .HasForeignKey(x => x.VisitorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
