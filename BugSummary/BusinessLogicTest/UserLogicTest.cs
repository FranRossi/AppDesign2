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
        [DataTestMethod]
        public void AddAdmin()
        {
            Mock<BugSummaryContext> _mockContext = new Mock<BugSummaryContext>(MockBehavior.Strict);
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

        [DataTestMethod]
        public void AddDeveloper()
        {
            Mock<BugSummaryContext> _mockContext = new Mock<BugSummaryContext>(MockBehavior.Strict);
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
    }
}
