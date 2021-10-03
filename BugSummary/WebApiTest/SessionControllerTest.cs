﻿using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Controllers;
using WebApi.Models;

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
            LoginModel loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };
            string mockedTokenResponse = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpX";
            Mock<ISessionLogic> mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedTokenResponse);
            SessionsController controller = new SessionsController(mock.Object);

            IActionResult result = controller.Post(loginModel);
            OkObjectResult createdResult = result as OkObjectResult;
            string tokenResponse = createdResult.Value as string;

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(mockedTokenResponse, tokenResponse);
        }

        [TestMethod]
        public void AuthenticateInvalidUser()
        {
            string username = "someUsername";
            string password = "somePassword";
            LoginModel loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };
            string mockedTokenResponse = null;
            Mock<ISessionLogic> mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedTokenResponse);
            SessionsController controller = new SessionsController(mock.Object);

            IActionResult result = controller.Post(loginModel);
            OkObjectResult createdResult = result as OkObjectResult;
            string tokenResponse = createdResult.Value as string;

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            Assert.AreEqual(mockedTokenResponse, tokenResponse);
        }
    }
}
