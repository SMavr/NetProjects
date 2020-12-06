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

            modelBuilder.Entity<Reach>()
                .Property(x => x.ReachId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Reach>()
                .HasData(
               new Reach { ReachId = (int) ReachId.Keynote, Description = "Keynote"  },
               new Reach { ReachId = (int) ReachId.Breakout, Description = "Breakout" },
               new Reach { ReachId = (int) ReachId.OpenSpace, Description = "Open Space"  }
               );
        }
    }
}
