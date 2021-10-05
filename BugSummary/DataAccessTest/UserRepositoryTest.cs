using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using DataAccess;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Utilities.Comparers;

namespace DataAccessTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        private readonly BugSummaryContext _bugSummaryContext;
        private readonly DbConnection _connection;
        private readonly DbContextOptions<BugSummaryContext> _contextOptions;
        private readonly UserRepository _userRepository;

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
            using (var context = new BugSummaryContext(_contextOptions))
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

            var userExpected = new List<User>();
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

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var usersDataBase = context.Users.ToList();
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
            using (var context = new BugSummaryContext(_contextOptions))
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

            var result = _userRepository.Authenticate(username, password);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void UserComparerTest()
        {
            using (var context = new BugSummaryContext(_contextOptions))
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

            var userExpected = new List<User>();
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

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var usersDataBase = context.Users.ToList();
                Assert.AreEqual(1, usersDataBase.Count());
                CollectionAssert.AreNotEqual(userExpected, usersDataBase, new UserComparer());
            }
        }

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX")]
        [DataRow(null)]
        [DataTestMethod]
        public void UpdateTokenTest(string token)
        {
            var newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newUser);
                context.SaveChanges();
            }

            newUser.Token = token;

            _userRepository.UpdateToken(newUser.UserName, newUser.Token);
            _userRepository.Save();

            using (var context = new BugSummaryContext(_contextOptions))
            {
                var databaseUser = context.Users.ToList().First(u => u.Id == newUser.Id);
                var compareLogic = new CompareLogic();
                var deepComparisonResult = compareLogic.Compare(newUser, databaseUser);
                Assert.IsTrue(deepComparisonResult.AreEqual);
            }
        }

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX", RoleType.Admin)]
        [DataRow("eydstdstdrstdrhrNiIsInRhstdarstd", RoleType.Developer)]
        [DataRow("342srtasrtars32rsdsrdasrdar44444", RoleType.Tester)]
        [DataTestMethod]
        public void GetRoleByValidTokenTest(string token, RoleType roleType)
        {
            var newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = roleType,
                Token = token
            };
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(newUser);
                context.SaveChanges();
            }

            var roleTypeResult = _userRepository.GetRoleByToken(token);

            Assert.AreEqual(roleType, roleTypeResult);
        }

        [TestMethod]
        public void GetRoleByInvalidValidTokenTest()
        {
            var roleTypeResult = _userRepository.GetRoleByToken(null);
            Assert.AreEqual(RoleType.Invalid, roleTypeResult);
        }

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX")]
        [DataTestMethod]
        public void GetUserByToken(string token)
        {
            var expected = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                Token = token
            };
            using (var context = new BugSummaryContext(_contextOptions))
            {
                context.Add(expected);
                context.SaveChanges();
            }

            var user = _userRepository.Get(token);

            Assert.AreEqual(expected.Id, user.Id);
        }

        [TestMethod]
        public void GetUserByInvalidValidTokenTest()
        {
            var result = _userRepository.Get(null);
            Assert.AreEqual(null, result);
        }
    }
}