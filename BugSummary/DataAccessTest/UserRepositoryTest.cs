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

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX", RoleType.Admin)]
        [DataRow("eydstdstdrstdrhrNiIsInRhstdarstd", RoleType.Developer)]
        [DataRow("342srtasrtars32rsdsrdasrdar44444", RoleType.Tester)]
        [DataTestMethod]
        public void GetRoleByValidTokenTest(string token, RoleType roleType)
        {
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = roleType,
                Token = token,
            };
            using (BugSummaryContext context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newUser);
                context.SaveChanges();
            }

            RoleType roleTypeResult = _userRepository.GetRoleByToken(token);

            Assert.AreEqual(roleType, roleTypeResult);
        }

        [TestMethod]
        public void GetRoleByInvalidValidTokenTest()
        {
            RoleType roleTypeResult = _userRepository.GetRoleByToken(null);
            Assert.AreEqual(RoleType.Invalid, roleTypeResult);
        }

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX")]
        [DataTestMethod]
        public void GetUserByToken(string token)
        {
            User expected = new User
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
            using (BugSummaryContext context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(expected);
                context.SaveChanges();
            }

            User user = _userRepository.Get(token);

            Assert.AreEqual(expected.Id, user.Id);
        }

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX")]
        [DataTestMethod]
        public void GetUserByTokenWithProjects(string token)
        {
            User expectedUser = new User
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
            Project newProject = new Project
            {
                Id = 1,
                Name = "Proyect 2344"
            };
            newProject.Users = new List<User> { expectedUser };
            using (BugSummaryContext context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newProject);
                context.SaveChanges();
            }

            User user = _userRepository.Get(token);

            expectedUser.Projects = new List<Project> { newProject };
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedUser, user);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetUserByInvalidValidTokenTest()
        {
            User result = _userRepository.Get(null);
            Assert.AreEqual(null, result);
        }
    }
}