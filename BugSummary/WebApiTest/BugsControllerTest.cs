using System.Collections;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
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
    public class BugsControllerTest
    {
        [TestMethod]
        public void GetBugsForTester()
        {
            UserModel userModel = new UserModel
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
            IEnumerable<Bug> bugsExpected = new List<Bug>()
            {
                new Bug()
                {
                    Id = 1,
                    Name = "Bug2021",
                    Description = "ImportanteBug",
                    Project = new Project(),
                    State = BugState.Active,
                    Version = "2",
                    ProjectId = 1,
                }
            };
            Mock<IBugLogic> mock = new Mock<IBugLogic>(MockBehavior.Strict);
            mock.Setup(r=>r.GetAll(It.IsAny<User>())).Returns(bugsExpected);
            BugsController controller = new BugsController(mock.Object);
            
            IActionResult result = controller.Get(userModel);
            OkObjectResult okResult = result as OkObjectResult;
            IEnumerable<Bug> bugsResult = okResult.Value as IEnumerable<Bug>;

            mock.VerifyAll();
            Assert.AreEqual(200,okResult.StatusCode);
            Assert.AreEqual(bugsExpected,bugsResult);
            CollectionAssert.AreEqual((ICollection) bugsExpected, (System.Collections.ICollection)bugsResult, new BugComparer());
        }
    }
}
