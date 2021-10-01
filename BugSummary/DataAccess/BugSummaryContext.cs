using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess
{
    public class BugSummaryContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Project> Projects { get; set; }

        public BugSummaryContext(DbContextOptions options) : base(options) { }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string directory = Directory.GetCurrentDirectory();

                IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(directory)
                .AddJsonFile("appsettings.json")
                .Build();

                string connectionString = configuration.GetConnectionString(@"BugDB");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}
