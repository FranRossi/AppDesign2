using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BugSummaryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bug> Bugs {  get; set; }

        public BugSummaryContext(DbContextOptions options) : base(options) { }


    }
}
