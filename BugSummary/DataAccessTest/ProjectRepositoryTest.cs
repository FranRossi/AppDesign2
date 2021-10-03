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
using Utilities.CustomExceptions;

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
            Project projectToAdd = new Project
            {
                Name = "New Project 2022"
            };

            _projectRepository.Add(projectToAdd);
            _projectRepository.Save();

            List<Project> projectsExpected = new List<Project>();
            projectsExpected.Add(new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug>(),
                Users = new List<User>()
            });
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<Project> projectsDataBase = context.Projects.ToList();
                Assert.AreEqual(1, projectsDataBase.Count());
                CollectionAssert.AreEqual(projectsExpected, projectsDataBase, new ProjectComparer());
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ProjectNameIsNotUniqueException))]
        public void AddAlreadyAddedProjectTest()
        {
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(new Project
                {
                    Name = "New Project 2022"
                });
                context.SaveChanges();
            }
            Project projectToAdd = new Project
            {
                Name = "New Project 2022"
            };

            try
            {
                _projectRepository.Add(projectToAdd);
            }
            catch (ProjectNameIsNotUniqueException e)
            {
                Assert.AreEqual("The project name chosen was already taken, please enter a different name", e.Message);
            }
        }

        [TestMethod]
        public void GetAllProjectsFromRepositoryTest()
        {
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(new Project
                {
                    Name = "New Project 2022",
                    Id = 1,
                    Bugs = new List<Bug>(),
                    Users = new List<User>()
                });
                context.SaveChanges();
                context.Add(new Project
                {
                    Name = "New Project 2023",
                    Id = 2,
                    Bugs = new List<Bug>(),
                    Users = new List<User>()
                });
                context.SaveChanges();

            }
            List<Project> projectsExpected = new List<Project>();
            projectsExpected.Add(new Project
            {
                Name = "New Project 2022",
                Id = 1,
                Bugs = new List<Bug>(),
                Users = new List<User>()
            });
            projectsExpected.Add(new Project
            {
                Name = "New Project 2023",
                Id = 2,
                Bugs = new List<Bug>(),
                Users = new List<User>()
            });


            List<Project> projectsDataBase = this._projectRepository.GetAll().ToList();

            Assert.AreEqual(2, projectsDataBase.Count());
            CollectionAssert.AreEqual(projectsExpected, projectsDataBase, new ProjectComparer());

        }

    }
}
