using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.DomainUtilities;
using System.Collections.Generic;
using System.Linq;
using Utilities.Comparers;

namespace DataAccessTest
{

    [TestClass]
    public class ProjectRepositoryTest
    {

        private readonly DbConnection _connection;
        private readonly ProjectRepository _projectRepository;
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;

        public ProjectRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(this._connection).Options;
            this._bugSummaryContext = new BugSummaryContext(this._contextOptions);
            this._projectRepository = new ProjectRepository(this._bugSummaryContext);
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
        public void AddNewProjectTest()
        {
            Project newProject = new Project
            {
                Name = "New Project 2022",
                Id = 1,
                BugId = new List<Bug>() { },
            };
            List<Project> projectsExpected = new List<Project>();
            projectsExpected.Add(newProject);

            this._projectRepository.Create(newProject);
            List<Project> projectsDataBase = this._projectRepository.GetAll().ToList();

            Assert.AreEqual(1, projectsDataBase.Count());
            CollectionAssert.AreEqual(projectsExpected, projectsDataBase, new ProjectComparer());

        }

        [TestMethod]
        public void GetAllProjectsFromRepositoryTest()
        {
            Project newProject = new Project
            {
                Name = "New Project 2022",
                Id = 1,
                BugId = new List<Bug>() { },
            };
            Project newProject2 = new Project
            {
                Name = "New Project 2023",
                Id = 2,
                BugId = new List<Bug>() { },
            };
            List<Project> projectsExpected = new List<Project>();
            projectsExpected.Add(newProject);


            this._projectRepository.Create(newProject);
            this._projectRepository.Create(newProject2);
            List<Project> projectsDataBase = this._projectRepository.GetAll().ToList();

            Assert.AreEqual(2, projectsDataBase.Count());
            CollectionAssert.AreEqual(projectsExpected, projectsDataBase, new ProjectComparer());

        }

    }
}
