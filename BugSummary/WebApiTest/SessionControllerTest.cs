using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiTest
{
    [TestClass]
    public class SessionControllerTest
    {
        [TestMethod]
        public void AuthenticateValidUser()
        {
            string username = "someUsername";
            string password = "somePassword";
            string mockedTokenResponse = "";
            Mock<ISessionLogic> mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>()))
                .Callback((string username, string password) => mockedTokenResponse = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX");
            SessionControler controller = new SessionControler(mock.Object);

            IActionResult result = controller.Post(username, password);
            CreatedAtRouteResult createdResult = result as CreatedAtRouteResult;
            string tokenResponse = createdResult.Value as string;

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(mockedTokenResponse, tokenResponse);
        }
    }
}
