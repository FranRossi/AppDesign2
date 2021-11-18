using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using DataAccess;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestUtilities;
using TestUtilities.Comparers;
using Utilities.CustomExceptions.DataAccess;

namespace DataAccessTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
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
            List<User> userExpected = new List<User>();
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
                HourlyRate = 0,
                Projects = new List<Project>()
            };
            _userRepository.Add(newUser);
            _userRepository.Save();

            using (var context = new BugSummaryContext(this._contextOptions))
            {
                List<User> usersDataBase = context.Users.ToList();
                Assert.AreEqual(1, usersDataBase.Count());
                userExpected.Add(newUser);
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

        [DataRow("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX")]
        [DataRow(null)]
        [DataTestMethod]
        public void UpdateInvalidUserToken(string token)
        {
            User newUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
                Token = token
            };

            TestExceptionUtils.Throws<InexistentUserException>(
                 () => _userRepository.UpdateToken(newUser.UserName, newUser.Token), "The entered user does not exist."
             );
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
        [DataRow("sthdciOi34253454543543stdasrdrst")]
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

            CompareLogic compareLogic = new CompareLogic();
            expected.Projects = new List<Project>();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expected, user);
            Assert.IsTrue(deepComparisonResult.AreEqual);
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

        [TestMethod]
        public void GetUserById()
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
                FixedBugs = new List<Bug>() { }
            };
            using (BugSummaryContext context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(expected);
                context.SaveChanges();
            }

            User user = _userRepository.Get(expected.Id);

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expected, user);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetUserWithBugsById()
        {
            Project projectTester = new Project()
            {
                Id = 1,
                Name = "Semester 2021",
            };
            Bug bug = new Bug
            {
                Id = 1,
                Name = "Bug1",
                Description = "Bug en el servidor",
                Version = "1.4",
                State = BugState.Active,
                ProjectId = 1
            };
            User expected = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                FixedBugs = new List<Bug>() { bug }
            };


            using (BugSummaryContext context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(projectTester);
                context.Add(expected);
                context.SaveChanges();
            }

            User user = _userRepository.Get(expected.Id);

            CompareLogic compareLogic = new CompareLogic();
            bug.Project = null;
            ComparisonResult deepComparisonResult = compareLogic.Compare(expected, user);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetInvalidUserById()
        {
            TestExceptionUtils.Throws<InexistentUserException>(
                 () => _userRepository.Get(1), "The entered user does not exist."
             );
        }

        [TestMethod]
        public void AddAlreadyAddedUserTest()
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
            User repeatedUser = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
                Projects = new List<Project>()
            };
            TestExceptionUtils.Throws<UsernameIsNotUniqueException>(
                 () => _userRepository.Add(repeatedUser), "The username chosen was already taken, please enter a different one."
             );
        }

        [TestMethod]
        public void GetProjectsAssignedToUser()
        {
            Project project1 = new Project()
            {
                Id = 1,
                Name = "Project 1",
                Bugs = new List<Bug>()
            };
            Project project2 = new Project()
            {
                Id = 2,
                Name = "Project 2",
                Bugs = new List<Bug>()
            };
            Project project3 = new Project()
            {
                Id = 3,
                Name = "Project 3",
                Bugs = new List<Bug>()
            };
            User user = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Token = "ATHNEIOARSH233tRASTtrs",
                Role = RoleType.Tester,
                Projects = new List<Project> { project1, project3 }
            };


            using (BugSummaryContext context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(project2);
                context.Add(user);
                context.SaveChanges();
            }

            IEnumerable<Project> projects = _userRepository.GetProjects(user.Token);

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(new List<Project> { project1, project3 }, projects);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetProjectsAssignedToAdmin()
        {
            Project project1 = new Project()
            {
                Id = 1,
                Name = "Project 1",
                Bugs = new List<Bug>()
            };
            Project project2 = new Project()
            {
                Id = 2,
                Name = "Project 2",
                Bugs = new List<Bug>(),
            };
            Project project3 = new Project()
            {
                Id = 3,
                Name = "Project 3",
                Bugs = new List<Bug>()
            };
            User user = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Token = "ATHNEIOARSH233tRASTtrs",
                Role = RoleType.Admin
            };


            using (BugSummaryContext context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(project1);
                context.Add(project2);
                context.Add(project3);
                context.Add(user);
                context.SaveChanges();
            }

            IEnumerable<Project> projects = _userRepository.GetProjects(user.Token);

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(new List<Project> { project1, project2, project3 }, projects);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            User newUser1 = new User
            {
                Id = 1,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
                HourlyRate = 34,
            };
            User newUser2 = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Gutierrez",
                Password = "juanoto",
                UserName = "llllllllllll",
                Email = "hola@gmail.com",
                Role = RoleType.Admin
            };
            User newUser3 = new User
            {
                Id = 3,
                FirstName = "Mario",
                LastName = "Kempes",
                Password = "marito24321",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                HourlyRate = 674,
            };
            using (var context = new BugSummaryContext(this._contextOptions))
            {
                context.Add(newUser1);
                context.Add(newUser2);
                context.Add(newUser3);
                context.SaveChanges();
            }
            List<User> userExpected = new List<User>();
            userExpected.Add(newUser1);
            userExpected.Add(newUser2);
            userExpected.Add(newUser3);

            IEnumerable<User> usersDataBase = this._userRepository.GetAll();

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(userExpected, usersDataBase);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

    }
}