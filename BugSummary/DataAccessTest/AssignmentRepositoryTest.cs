using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataAccess;
using Domain;
using Domain.DomainUtilities;
using Domain.DomainUtilities.CustomExceptions;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.Comparers;

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
        
        [TestMethod]
        public void AddNewAssignment()
        {
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                Project projectTester = new Project()
                {
                    Id = 1,
                    Name = "Semester 2021",
                    Users = new List<User>()
                };
                context.Projects.Add(projectTester);
                context.SaveChanges();
            };
            Assignment assignment = new Assignment
            {
                Id = 1,
                Name = "Bug1",
                Duration = 2,
                HourlyRate = 25,
                Project = new Project(),
                ProjectId = 1
            };
            List<Assignment> assignmentsExpected = new List<Assignment>();
            assignmentsExpected.Add(assignment);

            _assignmentRepository.Add(assignment);
            _assignmentRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<Assignment> assigmentsDataBase = context.Assignments.ToList();
                Assert.AreEqual(1, assigmentsDataBase.Count());
                CollectionAssert.AreEqual(assignmentsExpected, assigmentsDataBase, new AssignmentComparer());
            }
        }
        
    }
}