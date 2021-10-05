using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
            var user = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            var expectedUser = new User
            {
                Id = 0,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Admin
            };
            var mock = new Mock<ILogic<User>>(MockBehavior.Strict);
            User receivedUser = null;
            mock.Setup(m => m.Add(It.IsAny<User>())).Callback((User user) => receivedUser = user);
            var controller = new UsersController(mock.Object);

            var result = controller.Post(user);

            mock.VerifyAll();
            Assert.IsInstanceOfType(result, typeof(OkResult));
            var compareLogic = new CompareLogic();
            var deepComparisonResult = compareLogic.Compare(expectedUser, receivedUser);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void UserToModelTest()
        {
            var expectedUser = new User
            {
                Id = 0,
                FirstName = "Pepe",
                LastName = "Perez",
                Password = "pepe1234",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester
            };
            var userToCompare = new UserModel
            {
                FirstName = "Pepe",
                LastName = "Perez",
                UserName = "pp",
                Email = "pepe@gmail.com",
                Role = RoleType.Tester
            };
            var model = UserModel.ToModel(expectedUser);
            var compareLogic = new CompareLogic();
            var deepComparisonResult = compareLogic.Compare(userToCompare, model);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}