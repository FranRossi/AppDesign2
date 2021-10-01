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
                Role = RoleType.Admin
            };
            List<User> userExpected = new List<User>();
            userExpected.Add(newUser);

            _userRepository.Add(newUser);
            _userRepository.Save();
            List<User> usersDataBase = _userRepository.GetAll().ToList();

            Assert.AreEqual(1, usersDataBase.Count());
            CollectionAssert.AreEqual(userExpected, usersDataBase, new UserComparer());
        }

        [TestMethod]
        public void AuthenticateValidUser()
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

            string username = "pp";
            string password = "pepe1234";
            bool result = _userRepository.Authenticate(username, password);

            Assert.IsTrue(result);
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
                Role = RoleType.Admin
            };
            User newUser2 = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Rodriguez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            List<User> userExpected = new List<User>();
            userExpected.Add(newUser);

            _userRepository.Add(newUser2);
            _userRepository.Save();
            List<User> usersDataBase = _userRepository.GetAll().ToList();

            Assert.AreEqual(1, usersDataBase.Count());
            CollectionAssert.AreNotEqual(userExpected, usersDataBase, new UserComparer());
        }
    }
}
