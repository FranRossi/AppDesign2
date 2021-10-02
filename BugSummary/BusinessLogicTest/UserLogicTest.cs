using BusinessLogic;
using DataAccess;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class UserLogicTest
    {
        [TestMethod]
        public void AddAdmin()
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
            User receivedUser = null;
            Mock<IRepository<User>> _mockUserRepository = new Mock<IRepository<User>>(MockBehavior.Strict);
            _mockUserRepository.Setup(mr => mr.Add(It.IsAny<User>())).Callback((User newUser) =>
            {
                receivedUser = newUser;
            });
            _mockUserRepository.Setup(mr => mr.Save());


            UserLogic userLogic = new UserLogic(_mockUserRepository.Object);
            userLogic.Add(newUser);

            _mockUserRepository.VerifyAll();
            Assert.AreEqual(newUser, receivedUser);
        }

        [TestMethod]
        public void AddDeveloper()
        {
            User newUser = new User
            {
                Id = 2,
                FirstName = "Juan",
                LastName = "Sanchez",
                Password = "contra 123",
                UserName = "juanito",
                Email = "juan@gmail.com",
                Role = RoleType.Developer
            };
            User receivedUser = null;
            Mock<IRepository<User>> _mockUserRepository = new Mock<IRepository<User>>(MockBehavior.Strict);
            _mockUserRepository.Setup(mr => mr.Add(It.IsAny<User>())).Callback((User newUser) =>
            {
                receivedUser = newUser;
            });
            _mockUserRepository.Setup(mr => mr.Save());


            UserLogic userLogic = new UserLogic(_mockUserRepository.Object);
            userLogic.Add(newUser);

            _mockUserRepository.VerifyAll();
            Assert.AreEqual(newUser, receivedUser);
        }

        [TestMethod]
        public void AddTester()
        {
            User newUser = new User
            {
                Id = 24,
                FirstName = "Mario",
                LastName = "Fagundez",
                Password = "holasoyMario",
                UserName = "marito",
                Email = "mario@gmail.com",
                Role = RoleType.Tester
            };
            User receivedUser = null;
            Mock<IRepository<User>> _mockUserRepository = new Mock<IRepository<User>>(MockBehavior.Strict);
            _mockUserRepository.Setup(mr => mr.Add(It.IsAny<User>())).Callback((User newUser) =>
            {
                receivedUser = newUser;
            });
            _mockUserRepository.Setup(mr => mr.Save());


            UserLogic userLogic = new UserLogic(_mockUserRepository.Object);
            userLogic.Add(newUser);

            _mockUserRepository.VerifyAll();
            Assert.AreEqual(newUser, receivedUser);
        }
    }
}
