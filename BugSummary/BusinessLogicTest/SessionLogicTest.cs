using DataAccess;
using Domain;
using Domain.DomainUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Mock<UserRepository> mockUserRepository = new Mock<UserRepository>(MockBehavior.Strict);
            SessionLogic sessionLogic = new SessionLogic(mockUserRepository);

            string token = sessionLogic.GenerateToken();

            Assert.IsTrue(token.Length == TokenHelper.TokenLength);
        }
    }
}
