using Sample.Logger.DomainLayer;
using Sample.Users.DomainLayer;
using System.Data.Entity;

namespace Sample.Shared.InfrastructureLayer
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext() : base("SampleDbConnString")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<LogItem> Logs { get; set; }
    }
}
