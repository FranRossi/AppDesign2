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
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<BugSummaryContext>().UseSqlite(this._connection).Options;
            this._bugSummaryContext = new BugSummaryContext(this._contextOptions);
            this._userRepository = new UserRepository(this._bugSummaryContext);
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

            this._userRepository.Add(newUser);
            this._userRepository.Save();
            List<User> usersDataBase = this._userRepository.GetAll().ToList();

            Assert.AreEqual(1, usersDataBase.Count());
            CollectionAssert.AreEqual(userExpected, usersDataBase, new UserComparer());

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

            this._userRepository.Add(newUser2);
            this._userRepository.Save();
            List<User> usersDataBase = this._userRepository.GetAll().ToList();

            Assert.AreEqual(1, usersDataBase.Count());
            CollectionAssert.AreNotEqual(userExpected, usersDataBase, new UserComparer());
        }
    }
}
