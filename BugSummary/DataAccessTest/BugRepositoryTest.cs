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
    public class BugRepositoryTest
    {

        private readonly DbConnection _connection;
        private readonly BugRepository _bugRepository;
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;

        public BugRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(this._connection).Options;
            this._bugSummaryContext = new BugSummaryContext(this._contextOptions);
            this._bugRepository = new BugRepository(this._bugSummaryContext);
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
        public void AddNewBugTest()
        {
            Bug newBug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            List<Bug> bugsExpected = new List<Bug>();
            bugsExpected.Add(newBug);

            this._bugRepository.Add(newBug);
            this._bugRepository.Save();
            List<Bug> bugsDataBase = this._bugRepository.GetAll().ToList();

            Assert.AreEqual(1, bugsDataBase.Count());
            CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());

        }

        [TestMethod]
        public void GetAllBugsFromRepositoryTest()
        {
            Bug newBug1 = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            Bug newBug2 = new Bug
            {
                Id = 2,
                Name = "Bug2",
                Description = "Bug en el cliente",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            List<Bug> bugsExpected = new List<Bug>();
            bugsExpected.Add(newBug1);
            bugsExpected.Add(newBug2);


            this._bugRepository.Add(newBug1);
            this._bugRepository.Save();
            this._bugRepository.Add(newBug2);
            this._bugRepository.Save();
            List<Bug> bugsDataBase = this._bugRepository.GetAll().ToList();

            Assert.AreEqual(2, bugsDataBase.Count());
            CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());

        }

        [TestMethod]
        public void GetAllBugsFromTester()
        {
            User testerUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Projects = new List<Project>()
            };
            Project projectTester = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                 testerUser
                }
            };
            testerUser.Projects.Add(projectTester);
            Bug newBug1 = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = projectTester,
                ProjectId = 1
            };
            Bug newBug2 = new Bug
            {
                Id = 2,
                Name = "Bug2",
                Description = "Bug en el cliente",
                Version = "1.4",
                State = BugState.Active,
                Project = new Project(),
                ProjectId = 1
            };
            List<Bug> bugsExpected = new List<Bug>();
            bugsExpected.Add(newBug1);


            this._bugRepository.Add(newBug1);
            this._bugRepository.Save();
            this._bugRepository.Add(newBug2);
            this._bugRepository.Save();

            List<Bug> bugsDataBase = this._bugRepository.GetAllByTester(testerUser).ToList();

            Assert.AreEqual(1, bugsDataBase.Count());
            CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new BugComparer());

        }

        [ExpectedException(typeof(UserCannotCreateBugException))]
        public void DeveloperCreatesBug()
        {
            User devloperUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                Projects = new List<Project>()
            };
            Project projectTester = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
                Users = new List<User>
                {
                 devloperUser
                }
            };
            devloperUser.Projects.Add(projectTester);
            Bug newBug1 = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                Project = projectTester,
                ProjectId = 1
            };

            this._bugRepository.Add(devloperUser, newBug1);
            this._bugRepository.Save();

        }
    }
}
