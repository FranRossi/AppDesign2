using BusinessLogicInterface;
using Domain;
using Domain.DomainUtilities;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
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
        public void GetProjectsAssignedToUser()
        {
            IEnumerable<Project> projects = new List<Project>()
            {
                new Project
                    {
                        Id  = 1,
                        Name = "Project A"
                    },
                new Project
                    {
                        Id  = 2,
                        Name = "Project B"
                    },
                new Project
                    {
                        Id  = 3,
                        Name = "Project C"
                    }
            };
            string token = "Adstnhaioedn324f3";
            string receivedToken = "";
            IEnumerable<ProjectModel> expectedModel = ProjectModel.ToModelList(projects);
            Mock<IUserLogic> mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(m => m.GetProjects(It.IsAny<string>())).Returns(projects).Callback((string sentToken) => receivedToken = sentToken);
            ProjectsController controller = new ProjectsController(null, mock.Object);

            IActionResult result = controller.Get(token);
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<ProjectModel> projectResult = okResult.Value as IEnumerable<ProjectModel>;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(token, receivedToken);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, projectResult);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            IEnumerable<User> users = new List<User>()
            {
                new User
                {
                    Id = 1,
                    FirstName = "Pepe",
                    LastName = "Perez",
                    Password = "pepe1234",
                    UserName = "pp",
                    Email = "pepe@gmail.com",
                    Role = RoleType.Tester,
                    HourlyRate = 34,
                },
                new User
                {
                    Id = 2,
                    FirstName = "Juan",
                    LastName = "Gutierrez",
                    Password = "juanoto",
                    UserName = "llllllllllll",
                    Email = "hola@gmail.com",
                    Role = RoleType.Admin
                },
                new User
                {
                    Id = 3,
                    FirstName = "Mario",
                    LastName = "Kempes",
                    Password = "marito24321",
                    UserName = "pp",
                    Email = "pepe@gmail.com",
                    Role = RoleType.Developer,
                    HourlyRate = 674,
                }
            };
            IEnumerable<UserModel> expectedModel = UserModel.ToModelList(users);
            Mock<IUserLogic> mock = new Mock<IUserLogic>(MockBehavior.Strict);
            mock.Setup(r => r.GetAll()).Returns(users);
            UsersController controller = new UsersController(mock.Object);

            IActionResult result = controller.Get();
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<UserModel> userResult = okResult.Value as IEnumerable<UserModel>;

            mock.VerifyAll();
            Assert.AreEqual(200, okResult.StatusCode);
            CompareLogic compareLogic = new CompareLogic();
            ComparisonResult deepComparisonResult = compareLogic.Compare(expectedModel, userResult);
            Assert.IsTrue(deepComparisonResult.AreEqual);
        }
    }
}