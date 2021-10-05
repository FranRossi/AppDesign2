using System.IO;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class BugSummaryContext : DbContext
    {
        public BugSummaryContext()
        {
        }

        public BugSummaryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var directory = Directory.GetCurrentDirectory();

                var configuration = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                var connectionString = configuration.GetConnectionString(@"BugDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}