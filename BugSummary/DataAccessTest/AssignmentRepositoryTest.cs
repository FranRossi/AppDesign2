using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using DataAccess;
using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace DataAccessTest
{

    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AssignmentRepositoryTest
    {
        private readonly DbConnection _connection;
        private readonly AssignmentRepository _assignmentRepository;
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;

        public AssignmentRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(this._connection).Options;
            this._bugSummaryContext = new BugSummaryContext(this._contextOptions);
            this._assignmentRepository = new AssignmentRepository(this._bugSummaryContext);
        }

        [TestInitialize]
        public void Setup()
        {
            this._connection.Open();
            this._bugSummaryContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this._bugSummaryContext.Database.EnsureDeleted();
        }
        
    }
}