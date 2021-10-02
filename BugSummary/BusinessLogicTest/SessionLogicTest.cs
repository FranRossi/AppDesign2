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
            Mock<IRepository<User>> mockUserRepository = new Mock<IRepository<User>>(MockBehavior.Strict);
            SessionLogic sessionLogic = new SessionLogic(mockUserRepository.Object);

            string token = sessionLogic.GenerateToken();

            Assert.IsTrue(token.Length == TokenHelper.TokenLength);
        }
    }
}
