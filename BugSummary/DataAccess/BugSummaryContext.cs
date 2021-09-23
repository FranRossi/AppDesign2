using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class BugSummaryContext : DbContext
    {
        public BugSummaryContext(DbContextOptions options) : base(options) { }
    }
}
