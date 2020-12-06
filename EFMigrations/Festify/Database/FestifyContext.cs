using Microsoft.EntityFrameworkCore;

namespace Festify.Database
{
    public class FestifyContext : DbContext
    {
        public FestifyContext(DbContextOptions options) :
            base(options)
        {
        }

        public DbSet<Conference> Conference { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conference>()
                .HasAlternateKey(x => x.Identifier);

            modelBuilder.Entity<Session>()
                .HasAlternateKey(x => x.SessionGuid);
        }
    }
}
