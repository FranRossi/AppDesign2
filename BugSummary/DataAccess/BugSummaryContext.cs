using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BugSummaryContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public BugSummaryContext(DbContextOptions options) : base(options) { }


    }
}
