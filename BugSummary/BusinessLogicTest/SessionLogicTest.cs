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
            var mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var sessionLogic = new SessionLogic(mockUserRepository.Object);

            var token = sessionLogic.GenerateToken();

            Assert.IsTrue(token.Length == TokenHelper.TokenLength);
        }

        [TestMethod]
        public void CompareTokenUniqueness()
        {
            var mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var sessionLogic = new SessionLogic(mockUserRepository.Object);

            var firstToken = sessionLogic.GenerateToken();
            var secondToken = sessionLogic.GenerateToken();

            Assert.AreNotEqual(firstToken, secondToken);
        }

        [TestMethod]
        public void AuthenticateValidUser()
        {
            var mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var username = "someUsername";
            var password = "somePassword";
            mockUserRepository.Setup(mr => mr.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            mockUserRepository.Setup(mr => mr.Save());
            var sentUsername = "";
            var sentToken = "";
            mockUserRepository.Setup(mr => mr.UpdateToken(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string username, string token) =>
                {
                    sentUsername = username;
                    sentToken = token;
                });
            var _sessionLogic = new SessionLogic(mockUserRepository.Object);
            var result = _sessionLogic.Authenticate(username, password);

            Assert.AreNotEqual(null, result);
            mockUserRepository.Verify(mock => mock.UpdateToken(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.AreEqual(result, sentToken);
        }

        [TestMethod]
        [ExpectedException(typeof(LoginException))]
        public void AuthenticateInvalidUser()
        {
            var mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var username = "someUsername";
            var password = "somePassword";
            mockUserRepository.Setup(mr => mr.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
            mockUserRepository.Setup(mr => mr.UpdateToken(It.IsAny<string>(), It.IsAny<string>()));

            var sessionLogic = new SessionLogic(mockUserRepository.Object);
            sessionLogic.Authenticate(username, password);
        }

        [DataRow(RoleType.Admin)]
        [DataRow(RoleType.Tester)]
        [DataRow(RoleType.Developer)]
        [DataRow(RoleType.Invalid)]
        [DataTestMethod]
        public void GetRoleByToken(RoleType role)
        {
            var mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            var token = "someToken";
            var receivedToken = "";
            mockUserRepository.Setup(mr => mr.GetRoleByToken(It.IsAny<string>()))
                .Returns(role).Callback((string sentToken) => { receivedToken = sentToken; });

            var _sessionLogic = new SessionLogic(mockUserRepository.Object);
            var result = _sessionLogic.GetRoleByToken(token);

            Assert.AreEqual(role, role);
            mockUserRepository.Verify(mock => mock.GetRoleByToken(It.IsAny<string>()), Times.Once());
            Assert.AreEqual(token, receivedToken);
        }
    }
}