using Microsoft.EntityFrameworkCore;
using System;

namespace SpecTest
{
    public class SpecContext : DbContext
    {

        public SpecContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseInMemoryDatabase($"TestDatabase");

          
        }

        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Person>()
                .Property(p => p.Gender)
                .HasConversion(
                 v => v.ToString(),
                 v => (Gender)Enum.Parse(typeof(Gender), v));
        }
    }
}
