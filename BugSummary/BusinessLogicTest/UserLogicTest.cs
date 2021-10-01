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


            UserLogic userLogic = new UserLogic(_mockUserRepository.Object);
            userLogic.Create(newUser);

            _mockUserRepository.VerifyAll();
            Assert.AreEqual(newUser, receivedUser);
        }
    }
}
