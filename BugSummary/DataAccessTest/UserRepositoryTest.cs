using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain;
using System.Collections.Generic;
using System.Linq;
using Domain.DomainUtilities;
using Utilities.Comparers;
using KellermanSoftware.CompareNetObjects;

namespace DataAccessTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        private readonly DbConnection _connection;
        private readonly UserRepository _userRepository;
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;

        public UserRepositoryTest()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(_connection).Options;
            _bugSummaryContext = new BugSummaryContext(_contextOptions);
            _userRepository = new UserRepository(_bugSummaryContext);
        }

        [TestInitialize]
        public void Setup()
        {
            _connection.Open();
            _bugSummaryContext.Database.EnsureCreated();
        }

        [TestCleanup]
        public void CleanUp()
        {
            _bugSummaryContext.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AddNewUserTest()
        {

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(new User
                {
                    Id = 1,
                    FirstName = "Pepe",
                    LastName = "Perez",
                    Password = "pepe1234",
                    UserName = "pp",
                    Email = "pepe@gmail.com",
                    Role = RoleType.Admin,
                    Projects = new List<Project>()
                });
                context.SaveChanges();
            }
            List<User> userExpected = new List<User>();
            userExpected.Add(new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
                Projects = new List<Project>()
            });

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<User> usersDataBase = context.Users.ToList();
                Assert.AreEqual(1, usersDataBase.Count());
                CollectionAssert.AreEqual(userExpected, usersDataBase, new UserComparer());
            }
        }

        [DataRow("pp", "pepe1234", true)]
        [DataRow("Pp", "pepe1234", false)]
        [DataRow("pp", "Pepe1234", false)]
        [DataRow(" pp", "Pepe1234", false)]
        [DataRow("pp", " Pepe1234", false)]
        [DataRow("pp", "Pepe 1234", false)]
        [DataRow("", "pepe1234", false)]
        [DataRow("pp", "", false)]
        [DataRow("", "", false)]
        [DataTestMethod]
        public void AuthenticateUser(string username, string password, bool expectedResult)
        {
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(new User
                {
                    Id = 1,
                    FirstName = "Pepe",
                    LastName = "Perez",
                    Password = "pepe1234",
                    UserName = "pp",
                    Email = "pepe@gmail.com",
                    Role = RoleType.Admin
                });
                context.SaveChanges();
            }

            bool result = _userRepository.Authenticate(username, password);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void UserComparerTest()
        {
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(new User
                {
                    Id = 2,
                    FirstName = "Juan",
                    LastName = "Rodriguez",
                    Password = "pepe1234",
                    UserName = "pp",
                    Email = "pepe@gmail.com",
                    Role = RoleType.Admin,
                    Projects = new List<Project>()
                });
                context.SaveChanges();
            }
            List<User> userExpected = new List<User>();
            userExpected.Add(new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
                Projects = new List<Project>()
            });

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<User> usersDataBase = context.Users.ToList();
                Assert.AreEqual(1, usersDataBase.Count());
                CollectionAssert.AreNotEqual(userExpected, usersDataBase, new UserComparer());
            }
        }

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX")]
        [DataRow(null)]
        [DataTestMethod]
        public void UpdateTokenTest(string token)
        {
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newUser);
                context.SaveChanges();
            }
            newUser.Token = token;

            _userRepository.UpdateToken(newUser.UserName, newUser.Token);
            _userRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                User databaseUser = context.Users.ToList().First(u => u.Id == newUser.Id);
                CompareLogic compareLogic = new CompareLogic();
                ComparisonResult deepComparisonResult = compareLogic.Compare(newUser, databaseUser);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }
    }
}
