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
            User newUser = new User
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
            List<User> userExpected = new List<User>();
            userExpected.Add(newUser);

            _userRepository.Add(newUser);
            _userRepository.Save();
            List<User> usersDataBase = _userRepository.GetAll().ToList();

            Assert.AreEqual(1, usersDataBase.Count());
            CollectionAssert.AreEqual(userExpected, usersDataBase, new UserComparer());
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
            _userRepository.Add(newUser);
            _userRepository.Save();

            bool result = _userRepository.Authenticate(username, password);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void UserComparerTest()
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
                Projects = new List<Project>()
            };
            User newUser2 = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin,
                Projects = new List<Project>()
            };
            List<User> userExpected = new List<User>();
            userExpected.Add(newUser);

            _userRepository.Add(newUser2);
            _userRepository.Save();
            List<User> usersDataBase = _userRepository.GetAll().ToList();

            Assert.AreEqual(1, usersDataBase.Count());
            CollectionAssert.AreNotEqual(userExpected, usersDataBase, new UserComparer());
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
            _userRepository.Add(newUser);
            _userRepository.Save();
            newUser.Token = token;

            _userRepository.UpdateToken(newUser.UserName, newUser.Token);
            _userRepository.Save();
            User databaseUser = _userRepository.GetAll().ToList()[0];

            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(newUser, databaseUser);
            Assert.IsTrue(deepComparisonResult.AreEqual);
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
    }
}
