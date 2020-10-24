using Microsoft.EntityFrameworkCore;

namespace SpecTest
{
    public class SpecContext: DbContext
    {

        public SpecContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Person> Persons{ get; set; }
    }
}
