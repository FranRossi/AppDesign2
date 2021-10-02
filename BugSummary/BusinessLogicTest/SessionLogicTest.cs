using BusinessLogic;
using DataAccessInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilities.Authentication;

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
            mockUserRepository.Verify(mock => mock.UpdateToken(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
        }

        [TestMethod]
        public void AuthenticateInvalidUser()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            string username = "someUsername";
            string password = "somePassword";
            mockUserRepository.Setup(mr => mr.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);
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

            Assert.AreEqual(null, result);
            mockUserRepository.Verify(mock => mock.UpdateToken(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
            Assert.AreEqual(result, sentToken);
        }
    }
}
