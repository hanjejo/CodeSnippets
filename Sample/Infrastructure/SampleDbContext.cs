using Sample.UserService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Infrastructure
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext() : base("SampleDbConnString")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
