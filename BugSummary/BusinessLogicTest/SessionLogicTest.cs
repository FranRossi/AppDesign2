using BusinessLogic;
using DataAccess;
using DataAccessInterface;
using Domain;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            MethodInfo generateTokenMethod = sessionLogic.GetType().GetMethod("GenerateToken");

            string token = (string)generateTokenMethod.Invoke(sessionLogic, null);

            Assert.IsTrue(token.Length == TokenHelper.TokenLength);
        }

        [TestMethod]
        public void CompareTokenUniqueness()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            SessionLogic sessionLogic = new SessionLogic(mockUserRepository.Object);
            MethodInfo generateTokenMethod = sessionLogic.GetType().GetMethod("GenerateToken");

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

            SessionLogic _sessionLogic = new SessionLogic(mockUserRepository.Object);
            string result = _sessionLogic.Authenticate(username, password);

            Assert.AreNotEqual(null, result);
        }

        [TestMethod]
        public void AuthenticateInvalidUser()
        {
            Mock<IUserRepository> mockUserRepository = new Mock<IUserRepository>(MockBehavior.Strict);
            string username = "someUsername";
            string password = "somePassword";
            mockUserRepository.Setup(mr => mr.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            SessionLogic _sessionLogic = new SessionLogic(mockUserRepository.Object);
            string result = _sessionLogic.Authenticate(username, password);


            Assert.AreEqual(null, result);
        }
    }
}
