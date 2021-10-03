using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Utilities.Comparers;
using WebApi.Controllers;
using WebApi.Models;

namespace WebApiTest
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void AddValidUser()
        {
            UserModel user = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            User expectedUser = new User
            {
                Id = 0,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            Mock<ILogic<User>> mock = new Mock<ILogic<User>>(MockBehavior.Strict);
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
        public void UserToModelTest()
        {
            User expectedUser = new User
            {
                Id = 0,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester
            };
            UserModel userToCompare = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Perez",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester,
            };
            UserModel model = UserModel.ToModel(expectedUser);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(userToCompare, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}
