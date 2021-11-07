using BusinessLogicInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class SessionsControllerTest
    {
        [TestMethod]
        [ExcludeFromCodeCoverage]
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
            RoleType role = RoleType.Admin;
            AuthorizationModel modelExpected = AuthorizationModel.ToModel(mockedTokenResponse, role);
            Mock<ISessionLogic> mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedTokenResponse);
            SessionsController controller = new SessionsController(mock.Object);
            mock.Setup(m => m.GetRoleByToken(It.IsAny<string>())).Returns(role);
            

            IActionResult result = controller.Post(loginModel);
            OkObjectResult createdResult = result as OkObjectResult;
            AuthorizationModel authResponse = createdResult.Value as AuthorizationModel;

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(modelExpected, authResponse);
            Assert.IsTrue(deepComparisonResult.AreEqual);
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
            RoleType role = RoleType.Admin;
            AuthorizationModel modelExpected = AuthorizationModel.ToModel(mockedTokenResponse , role);
            Mock<ISessionLogic> mock = new Mock<ISessionLogic>(MockBehavior.Strict);
            mock.Setup(m => m.Authenticate(It.IsAny<string>(), It.IsAny<string>())).Returns(mockedTokenResponse);
            SessionsController controller = new SessionsController(mock.Object);
            mock.Setup(m => m.GetRoleByToken(It.IsAny<string>())).Returns(role);

            IActionResult result = controller.Post(loginModel);
            OkObjectResult createdResult = result as OkObjectResult;
            AuthorizationModel authResponse = createdResult.Value as AuthorizationModel;

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(modelExpected, authResponse);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
