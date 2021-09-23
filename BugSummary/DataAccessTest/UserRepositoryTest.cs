using DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Domain;
using Utilities;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Comparers;
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
                id = 1,
                firstName = "Pepe",
                lastName = "Perez",
                password = "pepe1234",
                userName = "pp",
                email = "pepe@gmail.com",
                role = RoleType.Admin
            };
            List<User> bugsExpected = new List<User>();
            bugsExpected.Add(newUser);

            this._bugSummaryContext.Add(newUser);
            this._bugSummaryContext.SaveChanges();
            List<User> bugsDataBase = this._userRepository.GetAll().ToList();

            Assert.AreEqual(1, bugsDataBase.Count());
            CollectionAssert.AreEqual(bugsExpected, bugsDataBase, new UserComparer());

        }
    }
}
