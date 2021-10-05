using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class SessionsControllerTest
    {
        [TestMethod]
        public void AuthenticateValidUser()
        {
            var username = "someUsername";
            var password = "somePassword";
            var loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };
            var mockedTokenResponse = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            var mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedTokenResponse);
            var controller = new SessionsController(mock.Object);

            var result = controller.Post(loginModel);
            var createdResult = result as OkObjectResult;
            var tokenResponse = createdResult.Value as string;

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(mockedTokenResponse, tokenResponse);
        }

        [TestMethod]
        public void AuthenticateInvalidUser()
        {
            var username = "someUsername";
            var password = "somePassword";
            var loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };
            string mockedTokenResponse = null;
            var mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedTokenResponse);
            var controller = new SessionsController(mock.Object);

            var result = controller.Post(loginModel);
            var createdResult = result as OkObjectResult;
            var tokenResponse = createdResult.Value as string;

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(mockedTokenResponse, tokenResponse);
        }
    }
}