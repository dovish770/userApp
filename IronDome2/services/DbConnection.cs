using IronDome2.Models;
using Microsoft.EntityFrameworkCore;

namespace IronDome2.services
{
    public class DbConnection : DbContext
    {
        public DbConnection(DbContextOptions<DbConnection> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Attack> Attacks { get; set; }
        public DbSet<LoginObject> loginObjects { get; set; }
    }
}
