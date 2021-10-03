using BusinessLogic;
using DataAccessInterface;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilities.Authentication;
using Utilities.CustomExceptions;

namespace BusinessLogicTest
{
    [TestClass]
    public class SessionLogicTest
    {
        [TestMethod]
        public void GetToken()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            SessionLogic sessionLogic = new SessionLogic(mockUserRepository.Object);

            string token = sessionLogic.GenerateToken();

            Assert.IsTrue(token.Length == TokenHelper.TokenLength);
        }

        [TestMethod]
        public void CompareTokenUniqueness()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            SessionLogic sessionLogic = new SessionLogic(mockUserRepository.Object);

            string firstToken = sessionLogic.GenerateToken();
            string secondToken = sessionLogic.GenerateToken();

            Assert.AreNotEqual(firstToken, secondToken);
        }

        [TestMethod]
        public void AuthenticateValidUser()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            string username = "someUsername";
            string password = "somePassword";
            mockUserRepository.Setup(mr => mr.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            mockUserRepository.Setup(mr => mr.Save());
            string sentUsername = "";
            string sentToken = "";
            mockUserRepository.Setup(mr => mr.UpdateToken(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string username, string token) =>
                {
                    sentUsername = username;
                    sentToken = token;
                });
            SessionLogic _sessionLogic = new SessionLogic(mockUserRepository.Object);
            string result = _sessionLogic.Authenticate(username, password);

            Assert.AreNotEqual(null, result);
            mockUserRepository.Verify(mock => mock.UpdateToken(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.AreEqual(result, sentToken);
        }

        [TestMethod]
        [ExpectedException(typeof(LoginException))]
        public void AuthenticateInvalidUser()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            string username = "someUsername";
            string password = "somePassword";
            mockUserRepository.Setup(mr => mr.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            mockUserRepository.Setup(mr => mr.UpdateToken(It.IsAny<string>(), It.IsAny<string>()));

            SessionLogic sessionLogic = new SessionLogic(mockUserRepository.Object);
            sessionLogic.Authenticate(username, password);
        }

        [DataRow(RoleType.Admin)]
        [DataRow(RoleType.Tester)]
        [DataRow(RoleType.Developer)]
        [DataRow(RoleType.Invalid)]
        [DataTestMethod]
        public void GetRoleByToken(RoleType role)
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            string token = "someToken";
            string receivedToken = "";
            mockUserRepository.Setup(mr => mr.GetRoleByToken(It.IsAny<string>()))
                .Returns(role).Callback((string sentToken) =>
                {
                    receivedToken = sentToken;
                });

            SessionLogic _sessionLogic = new SessionLogic(mockUserRepository.Object);
            RoleType result = _sessionLogic.GetRoleByToken(token);

            Assert.AreEqual(role, role);
            mockUserRepository.Verify(mock => mock.GetRoleByToken(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(token, receivedToken);
        }
    }
}
