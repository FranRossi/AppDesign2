using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UsersControllerTest
    {
        [TestMethod]
        public void AddUser()
        {
            UserModel user = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Developer,
                HourlyRate = 241
            };
            User expectedUser = new User
            {
                Id = 0,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                HourlyRate = 241,
                Role = RoleType.Developer
            };
            Mock<IUserLogic> mock = new Mock<IUserLogic>(MockBehavior.Strict);
            User receivedUser = null;
            mock.Setup(m => m.Add(It.IsAny<User>())).Callback((User user) => receivedUser = user);
            UsersController controller = new UsersController(mock.Object);

            IActionResult result = controller.Post(user);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedUser, receivedUser);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetFixedBugCount()
        {
            int id = 1;
            int receivedId = -1;
            int expectedResult = 3;
            Mock<IUserLogic> mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetFixedBugCount(It.IsAny<int>())).Returns(expectedResult).Callback((int sentId) => receivedId = sentId);
            UsersController controller = new UsersController(mock.Object);

            IActionResult result = controller.Get(id);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            OkObjectResult createdResult = result as OkObjectResult;
            int response = (int)createdResult.Value;
            Assert.AreEqual(id, receivedId);
            Assert.AreEqual(expectedResult, response);
        }
    }
}